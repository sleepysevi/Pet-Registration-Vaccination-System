using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using AlagaTrack;
using AlagaTrackFrontEnd;

namespace MainPages
{
    public partial class Dashboard : Form
    {
        private int radius = 18;

        private PictureBox ProfileIconBox;

        private Label TotalPetsNum;
        private Label VaccinatedPercentage;
        private Label LostNum;

        private Label TotalPetsToday;
        private Label VaccinatedRatio;
        private Label LostStatus;

        private Panel RecentPetsPanel;
        private Label RecentPetsTitle;
        private TableLayoutPanel RecentPetsTable;
        private Panel SeeMorePanel;
        private Label SeeMoreLabel;

        private Panel AboutClick;
        private Label AboutLabel;

        private Panel ChartContainer;

        private SimpleLineChart chart;

        public Dashboard()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            Header.Paint += Header_Paint;
            this.Paint += Dashboard_Paint;

            ApplyPanelStyle(TotalPetsDash);
            ApplyPanelStyle(VaccinatedDash);
            ApplyPanelStyle(LostDash);

            CreateProfileIcon();
            CreateChart();
            CreateNumbers();
            CreateExtraLabels();
            CreateRecentPetsPanel();
            CreateAboutPanel();

            this.Shown += (s, e) =>
            {
                CenterMain();
                CenterExtras();
                RefreshFromDatabase();
                if (!TopLevel)
                    AboutClick?.BringToFront();
            };
        }

        private void RefreshFromDatabase()
        {
            var petMgr = new PetManager();
            var vacMgr = new VaccinationManager();
            var lostMgr = new LostReportManager();

            int totalPets = petMgr.GetTotalPetCount();
            int todayPets = petMgr.GetPetsRegisteredTodayCount();
            TotalPetsNum.Text = totalPets.ToString();
            TotalPetsToday.Text = $"+{todayPets} today";

            var (vacPets, vacTotal) = vacMgr.GetVaccinationCoverage();
            int pct = vacTotal <= 0 ? 0 : (int)Math.Round(100.0 * vacPets / vacTotal);
            VaccinatedPercentage.Text = $"{pct}%";
            VaccinatedRatio.Text = $"{vacPets}/{Math.Max(vacTotal, 1)}";

            int activeLost = lostMgr.CountByStatus("active");
            LostNum.Text = activeLost.ToString();
            LostStatus.Text = activeLost == 0 ? "None active" : "Active";

            int year = DateTime.Now.Year;
            var monthly = vacMgr.GetShotsPerMonthForYear(year);
            chart.Values = monthly;
            int peak = monthly.Length == 0 ? 0 : monthly.Max();
            chart.ValueMax = peak <= 0 ? 1 : peak;
            chart.Invalidate();

            var recent = petMgr.GetRecentPets(8);
            var vacInfo = vacMgr.GetLatestVaccinationSummaryByPetIds(recent.Select(p => p.PetID));
            for (int r = 1; r < 9; r++)
            {
                string id = "-", name = "-", owner = "-", lastVac = "-", st = "-";
                if (r - 1 < recent.Count)
                {
                    var p = recent[r - 1];
                    id = p.PetID.ToString();
                    name = string.IsNullOrWhiteSpace(p.PetName) ? "—" : p.PetName;
                    owner = string.IsNullOrWhiteSpace(p.OwnerName) ? "—" : p.OwnerName;
                    if (vacInfo.TryGetValue(p.PetID, out var vi))
                    {
                        lastVac = vi.lastDate;
                        st = vi.status;
                    }
                    else
                    {
                        lastVac = "—";
                        st = "No shot";
                    }
                }

                SetRecentCell(r, 0, id);
                SetRecentCell(r, 1, name);
                SetRecentCell(r, 2, owner);
                SetRecentCell(r, 3, lastVac);
                SetRecentCell(r, 4, st);
            }

            CenterMain();
            CenterExtras();
        }

        private void SetRecentCell(int row, int col, string text)
        {
            var ctrl = RecentPetsTable.GetControlFromPosition(col, row);
            if (ctrl is Label lbl) lbl.Text = text;
        }

        private void CreateProfileIcon()
        {
            Image profileImage;
            try
            {
                profileImage = Image.FromFile(@"Resources/ProfileIcon.png");
            }
            catch
            {
                profileImage = AlagaTrackFrontEnd.Dashboard.MainPages.Properties.Resources.OfficialSingleAlagaTrackIcon;
            }

            ProfileIconBox = new PictureBox
            {
                Image = profileImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            // auto-fit image size
            ProfileIconBox.Size = ProfileIconBox.Image.Size;

            // position
            ProfileIconBox.Location = new Point(890, 18);

            Controls.Add(ProfileIconBox);
        }
        private Label CreateTableText(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White
            };
        }
        private void CreateRecentPetsPanel()
        {
            RecentPetsPanel = new Panel
            {
                Size = new Size(460, 260),
                Location = new Point(ChartContainer.Right + 20, ChartContainer.Top),
                BackColor = Color.White
            };

            Controls.Add(RecentPetsPanel);

            ApplyPanelStyle(RecentPetsPanel);

            // TITLE
            RecentPetsTitle = new Label
            {
                Text = "Recent Pets",
                Font = new Font("Inter", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true
            };

            RecentPetsPanel.Controls.Add(RecentPetsTitle);

            RecentPetsTitle.Location = new Point(
                (RecentPetsPanel.Width - RecentPetsTitle.Width) / 2,
                10
            );

            // TABLE
            RecentPetsTable = new TableLayoutPanel
            {
                Size = new Size(440, 170),
                Location = new Point(10, 40),
                ColumnCount = 5,
                RowCount = 9,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            RecentPetsTable.ColumnStyles.Clear();

            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // ID
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // NAME
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // OWNER
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28)); // LAST VACCINE
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18)); // STATUS

            string[] headers = { "ID", "NAME", "OWNER", "LAST VACCINE", "STATUS" };

            for (int c = 0; c < 5; c++)
                RecentPetsTable.Controls.Add(CreateTableText(headers[c]), c, 0);

            for (int r = 1; r < 9; r++)
                for (int c = 0; c < 5; c++)
                    RecentPetsTable.Controls.Add(CreateTableText("-"), c, r);

            RecentPetsPanel.Controls.Add(RecentPetsTable);

            // SEE MORE
            SeeMorePanel = new Panel
            {
                Size = new Size(140, 30),
                BackColor = Color.FromArgb(254, 207, 106)
            };

            ApplyPanelStyle(SeeMorePanel);

            SeeMoreLabel = new Label
            {
                Text = "See more pets",
                Font = new Font("Inter", 10F, FontStyle.Bold),
                AutoSize = true
            };

            SeeMorePanel.Controls.Add(SeeMoreLabel);

            SeeMoreLabel.Location = new Point(
                (SeeMorePanel.Width - SeeMoreLabel.Width) / 2,
                (SeeMorePanel.Height - SeeMoreLabel.Height) / 2
            );

            SeeMorePanel.Location = new Point(
                (RecentPetsPanel.Width - SeeMorePanel.Width) / 2,
                220
            );

            RecentPetsPanel.Controls.Add(SeeMorePanel);

            RecentPetsTable.CellPaint += RecentPetsTable_CellPaint;

            SeeMorePanel.Cursor = Cursors.Hand;
            SeeMoreLabel.Cursor = Cursors.Hand;
            void GoPets(object s, EventArgs ev) => GetShell()?.Navigate("Pets");
            SeeMorePanel.Click += GoPets;
            SeeMoreLabel.Click += GoPets;
        }
        private void RecentPetsTable_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen p = new Pen(Color.Black, 1))
            {
                Rectangle r = e.CellBounds;
                e.Graphics.DrawRectangle(p, r);
            }
        }
        private void CreateAboutPanel()
        {
            AboutLabel = new Label
            {
                Text = "ABOUT",
                Font = new Font("Inter", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            int padX = 18;
            int padY = 10;
            int innerW = Math.Max(AboutLabel.PreferredWidth, 48);
            int innerH = Math.Max(AboutLabel.PreferredHeight, 20);
            AboutClick = new Panel
            {
                BackColor = Color.FromArgb(32, 47, 124),
                Location = new Point(760, 28),
                Size = new Size(innerW + padX * 2, innerH + padY * 2),
                Cursor = Cursors.Hand
            };

            // Draw rounded corners only — do not set Region (it shrinks hit-testing and breaks clicks).
            AboutClick.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = AboutClick.ClientRectangle;
                rect.Width--;
                rect.Height--;
                using (GraphicsPath path = RoundedRect(rect, 20))
                using (var brush = new SolidBrush(AboutClick.BackColor))
                    e.Graphics.FillPath(brush, path);
            };

            AboutLabel.Location = new Point(
                (AboutClick.Width - AboutLabel.Width) / 2,
                (AboutClick.Height - AboutLabel.Height) / 2
            );

            AboutClick.Click += OpenAboutPage;
            AboutLabel.Click += OpenAboutPage;
            AboutClick.Controls.Add(AboutLabel);
            Controls.Add(AboutClick);
            AboutClick.BringToFront();
        }
        private void OpenAboutPage(object sender, EventArgs e)
        {
            GetShell()?.Navigate("About");
        }

        /// <summary>Main shell (Form1). FindForm() returns this embedded Form, not Form1.</summary>
        private Form1? GetShell() => Parent?.TopLevelControl as Form1;

        // ================= CHART =================
        private void CreateChart()
        {
            ChartContainer = new Panel
            {
                Size = new Size(460, 260), 
                Location = new Point(20, 300),
                BackColor = Color.White
            };

            Controls.Add(ChartContainer);
            ApplyPanelStyle(ChartContainer);

            // CHART INSIDE 
            chart = new SimpleLineChart
            {
                Size = new Size(420, 220), 
                Location = new Point(20, 20), 
                Values = Array.Empty<int>()
            };

            ChartContainer.Controls.Add(chart);
        }

        public class SimpleLineChart : Control
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int[] Values { get; set; } = Array.Empty<int>();

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int ValueMax { get; set; } = 100;

            private readonly string[] months =
            {
        "Jan","Feb","Mar","Apr","May","Jun",
        "Jul","Aug","Sep","Oct","Nov","Dec"
    };

            private int radius = 18;

            public SimpleLineChart()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = ClientRectangle;

                int paddingTop = 45;
                int paddingBottom = 45; 
                int paddingLeft = 40;
                int paddingRight = 20;

                Rectangle chartArea = new Rectangle(
                    rect.Left + paddingLeft,
                    rect.Top + paddingTop,
                    rect.Width - paddingLeft - paddingRight,
                    rect.Height - paddingTop - paddingBottom
                );

                // ================= DROP SHADOW =================
                DrawChartShadow(g, rect);

                // ================= ROUNDED BACKGROUND =================
                using (Brush bg = new SolidBrush(Color.White))
                using (GraphicsPath path = RoundedRect(rect, radius))
                {
                    g.FillPath(bg, path);
                }

                // ================= TITLE (CENTERED) =================
                using (Font titleFont = new Font("Inter", 16F, FontStyle.Bold))
                using (Brush titleBrush = new SolidBrush(Color.FromArgb(71, 85, 105)))
                {
                    string title = "Vaccination Trends Over Time";
                    SizeF size = g.MeasureString(title, titleFont);

                    g.DrawString(
                        title,
                        titleFont,
                        titleBrush,
                        (rect.Width - size.Width) / 2,
                        10
                    );
                }

                // ================= GRID =================
                using (Pen gridPen = new Pen(Color.FromArgb(235, 235, 235)))
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        int y = chartArea.Top + (chartArea.Height / 5) * i;
                        g.DrawLine(gridPen, chartArea.Left, y, chartArea.Right, y);
                    }
                }

                // ================= AXIS LABELS =================
                using (Font axisFont = new Font("Inter", 12F, FontStyle.Bold))
                using (Brush axisBrush = new SolidBrush(Color.FromArgb(71, 85, 105)))
                {
                    // X AXIS (MONTHS) — SPACING ni ha
                    for (int i = 0; i < months.Length; i++)
                    {
                        float x = chartArea.Left +
                                  (chartArea.Width / 11f) * i; // spread more evenly

                        SizeF textSize = g.MeasureString(months[i], axisFont);

                        g.DrawString(
                            months[i],
                            axisFont,
                            axisBrush,
                            x - (textSize.Width / 2),
                            chartArea.Bottom + 8
                        );
                    }

                    int yMax = Math.Max(1, ValueMax);
                    // Y AXIS VALUES
                    for (int i = 0; i <= 5; i++)
                    {
                        int val = (int)Math.Round(yMax * (5 - i) / 5.0);
                        float y = chartArea.Top + (chartArea.Height / 5f) * i;

                        g.DrawString(
                            val.ToString(),
                            axisFont,
                            axisBrush,
                            8,
                            y - 10
                        );
                    }
                }

                // ================= NO DATA STATE =================
                if (Values == null || Values.Length == 0)
                {
                    using (Font f = new Font("Inter", 12F, FontStyle.Bold))
                    using (Brush b = new SolidBrush(Color.Gray))
                    {
                        string msg = "No data yet";
                        SizeF size = g.MeasureString(msg, f);

                        g.DrawString(
                            msg,
                            f,
                            b,
                            (rect.Width - size.Width) / 2,
                            (rect.Height - size.Height) / 2
                        );
                    }
                    return;
                }

                int max = Math.Max(1, ValueMax);
                PointF[] points = new PointF[Values.Length];
                float denom = Values.Length <= 1 ? 1f : (Values.Length - 1f);

                for (int i = 0; i < Values.Length; i++)
                {
                    float x = chartArea.Left + (chartArea.Width / denom) * i;
                    float y = chartArea.Bottom - (Values[i] / (float)max * chartArea.Height);
                    points[i] = new PointF(x, y);
                }

                using (Pen linePen = new Pen(Color.DodgerBlue, 3))
                {
                    g.DrawLines(linePen, points);
                }

                using (Brush dot = new SolidBrush(Color.DodgerBlue))
                {
                    foreach (var p in points)
                        g.FillEllipse(dot, p.X - 3, p.Y - 3, 6, 6);
                }
            }

            // ================= DROP SHADOW =================
            private void DrawChartShadow(Graphics g, Rectangle rect)
            {
                int blur = 10;
                int offsetY = 5;
                int baseAlpha = 10;

                for (int i = 0; i < blur; i++)
                {
                    int alpha = (int)(baseAlpha * (1f - (i / (float)blur)));

                    using (Brush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                    {
                        Rectangle shadow = new Rectangle(
                            rect.X + i,
                            rect.Y + offsetY + i,
                            rect.Width - (i * 2),
                            rect.Height - (i * 2)
                        );

                        using (GraphicsPath path = RoundedRect(shadow, radius))
                        {
                            g.FillPath(b, path);
                        }
                    }
                }
            }


            // ================= ROUNDED RECT =================
            private GraphicsPath RoundedRect(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                int d = radius * 2;

                path.StartFigure();
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();

                return path;
            }

           

        }

        // ================= PANELS =================
        private void ApplyPanelStyle(Panel p)
        {
            if (p == null) return;

            p.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = RoundedRect(p.ClientRectangle, radius))
                {
                    p.Region = new Region(path);
                }
            };
        }

        // ================= SHADOW =================
        private void Dashboard_Paint(object sender, PaintEventArgs e)
        {
            DrawShadow(e.Graphics, TotalPetsDash);
            DrawShadow(e.Graphics, VaccinatedDash);
            DrawShadow(e.Graphics, LostDash);
            DrawShadow(e.Graphics, Header);
            DrawShadow(e.Graphics, AboutClick);
            DrawShadow(e.Graphics, SeeMorePanel);
            DrawShadow(e.Graphics, RecentPetsPanel);
            DrawShadow(e.Graphics, ChartContainer);
        }

        private void DrawShadow(Graphics g, Panel panel)
        {
            Rectangle r = panel.Bounds;

            int blur = 10;
            int offsetY = 4;
            int baseAlpha = 10;

            for (int i = 0; i < blur; i++)
            {
                int alpha = (int)(baseAlpha * (1f - i / (float)blur));

                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
                {
                    Rectangle shadow = new Rectangle(
                        r.X,
                        r.Y + offsetY + i,
                        r.Width,
                        r.Height
                    );

                    FillRounded(g, b, shadow, radius);
                }
            }
        }

        // ================= HEADER =================
        private void Header_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = Header.ClientRectangle;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(210, 227, 255),
                Color.FromArgb(63, 94, 197),
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = RoundedRect(rect, 22))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        // ================= ROUND =================
        private GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void FillRounded(Graphics g, Brush b, Rectangle r, int radius)
        {
            using (GraphicsPath path = RoundedRect(r, radius))
                g.FillPath(b, path);
        }

        // ================= LABELS =================
        private void CreateNumbers()
        {
            TotalPetsNum = CreateNumber("0", Color.FromArgb(30, 64, 175), TotalPetsDash);
            VaccinatedPercentage = CreateNumber("0%", Color.FromArgb(5, 150, 105), VaccinatedDash);
            LostNum = CreateNumber("0", Color.FromArgb(220, 38, 38), LostDash);
        }

        private void CreateExtraLabels()
        {
            TotalPetsToday = CreateSmall("+0 today", Color.FromArgb(101, 137, 255), TotalPetsDash);
            VaccinatedRatio = CreateSmall("0/0", Color.FromArgb(65, 206, 159), VaccinatedDash);
            LostStatus = CreateSmall("Active", Color.FromArgb(217, 115, 115), LostDash);
        }

        private Label CreateNumber(string text, Color color, Panel parent)
        {
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Font = new Font("Microsoft Sans Serif", 40F, FontStyle.Bold, GraphicsUnit.Pixel);
            lbl.ForeColor = color;
            lbl.Text = text;
            parent.Controls.Add(lbl);
            return lbl;
        }

        private Label CreateSmall(string text, Color color, Panel parent)
        {
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Font = new Font("Inter", 14F, FontStyle.Bold);
            lbl.ForeColor = color;
            lbl.Text = text;
            parent.Controls.Add(lbl);
            return lbl;
        }

        // ================= CENTER =================
        private void CenterMain()
        {
            TotalPetsNum.Location = new Point((TotalPetsDash.Width - TotalPetsNum.Width) / 2, 40);
            VaccinatedPercentage.Location = new Point((VaccinatedDash.Width - VaccinatedPercentage.Width) / 2, 40);
            LostNum.Location = new Point((LostDash.Width - LostNum.Width) / 2, 40);
        }

        private void CenterExtras()
        {
            TotalPetsToday.Location = new Point((TotalPetsDash.Width - TotalPetsToday.Width) / 2, 85);
            VaccinatedRatio.Location = new Point((VaccinatedDash.Width - VaccinatedRatio.Width) / 2, 85);
            LostStatus.Location = new Point((LostDash.Width - LostStatus.Width) / 2, 85);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Hosted inside Form1: shell already has hamburger, title, and profile — hide duplicate chrome.
            if (!TopLevel)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                if (ProfileIconBox != null)
                    ProfileIconBox.Visible = false;

                const int dy = -68;
                void Up(Control? c)
                {
                    if (c != null) c.Top += dy;
                }

                Up(Header);
                Up(TotalPetsDash);
                Up(VaccinatedDash);
                Up(LostDash);
                Up(ChartContainer);
                Up(RecentPetsPanel);
                if (AboutClick != null)
                    AboutClick.Top = Math.Max(8, AboutClick.Top + dy);

                AboutClick?.BringToFront();
            }
        }
    }
}