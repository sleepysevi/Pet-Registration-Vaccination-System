using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
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

            ApplyPanelStyle(TotalPetsDash);
            ApplyPanelStyle(VaccinatedDash);
            ApplyPanelStyle(LostDash);

            CreateProfileIcon();
            CreateChart();
            CreateNumbers();
            CreateExtraLabels();
            CreateRecentPetsPanel();

            this.Shown += async (s, e) =>
            {
                CenterMain();
                CenterExtras();
                await RefreshFromDatabaseAsync();
            };
        }

        private async Task RefreshFromDatabaseAsync()
        {
            var data = await Task.Run(() => FetchDashboardData());
            ApplyDashboardData(data);
        }

        private DashboardData FetchDashboardData()
        {
            var petMgr = new PetManager();
            var vacMgr = new VaccinationManager();
            var lostMgr = new LostReportManager();

            int totalPets = petMgr.GetTotalPetCount();
            int todayPets = petMgr.GetPetsRegisteredTodayCount();
            var (vacPets, vacTotal) = vacMgr.GetVaccinationCoverage();
            int activeLost = lostMgr.CountByStatus("active");
            int year = DateTime.Now.Year;
            var monthly = vacMgr.GetShotsPerMonthForYear(year);
            var recent = petMgr.GetRecentPets(8);
            var vacInfo = vacMgr.GetLatestVaccinationSummaryByPetIds(recent.Select(p => p.PetID));

            return new DashboardData(totalPets, todayPets, vacPets, vacTotal, activeLost, monthly, recent, vacInfo);
        }

        private void ApplyDashboardData(DashboardData data)
        {
            TotalPetsNum.Text = data.TotalPets.ToString();
            TotalPetsToday.Text = $"+{data.TodayPets} today";

            int pct = data.TotalVaccTotal <= 0 ? 0 : (int)Math.Round(100.0 * data.VaccinatedPets / data.TotalVaccTotal);
            VaccinatedPercentage.Text = $"{pct}%";
            VaccinatedRatio.Text = $"{data.VaccinatedPets}/{Math.Max(data.TotalVaccTotal, 1)}";

            LostNum.Text = data.ActiveLost.ToString();
            LostStatus.Text = data.ActiveLost == 0 ? "None active" : "Active";

            chart.Values = data.Monthly;
            int peak = data.Monthly.Length == 0 ? 0 : data.Monthly.Max();
            chart.ValueMax = peak <= 0 ? 1 : peak;
            chart.Invalidate();

            for (int r = 1; r < 9; r++)
            {
                string id = "-", name = "-", owner = "-", lastVac = "-", st = "-";
                if (r - 1 < data.RecentPets.Count)
                {
                    var p = data.RecentPets[r - 1];
                    id = p.PetID.ToString();
                    name = string.IsNullOrWhiteSpace(p.PetName) ? "—" : p.PetName;
                    owner = string.IsNullOrWhiteSpace(p.OwnerName) ? "—" : p.OwnerName;
                    if (data.VaccinationSummaries.TryGetValue(p.PetID, out var vi))
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

        private sealed record DashboardData(
            int TotalPets,
            int TodayPets,
            int VaccinatedPets,
            int TotalVaccTotal,
            int ActiveLost,
            int[] Monthly,
            List<PetRecord> RecentPets,
            Dictionary<int, (string lastDate, string status)> VaccinationSummaries);

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
        private Label CreateTableText(string text, bool isHeader = false)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", isHeader ? 9F : 8.25F, isHeader ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isHeader ? UiTheme.TextPrimary : UiTheme.TextSecondary,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                AutoEllipsis = true,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
        }
        private void CreateRecentPetsPanel()
        {
            RecentPetsPanel = new Panel
            {
                Size = new Size(460, 300),
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
                Size = new Size(440, 232),
                Location = new Point(10, 40),
                ColumnCount = 5,
                RowCount = 9,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                BackColor = Color.White
            };
            RecentPetsTable.ColumnStyles.Clear();
            RecentPetsTable.RowStyles.Clear();
            RecentPetsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            for (int i = 1; i < 9; i++)
                RecentPetsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));

            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15)); // ID
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // NAME
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25)); // OWNER
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28)); // LAST VACCINE
            RecentPetsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18)); // STATUS

            string[] headers = { "ID", "NAME", "OWNER", "LAST VACCINE", "STATUS" };

            for (int c = 0; c < 5; c++)
                RecentPetsTable.Controls.Add(CreateTableText(headers[c], isHeader: true), c, 0);

            for (int r = 1; r < 9; r++)
                for (int c = 0; c < 5; c++)
                    RecentPetsTable.Controls.Add(CreateTableText("-"), c, r);

            RecentPetsPanel.Controls.Add(RecentPetsTable);

            // SEE MORE
            SeeMorePanel = new Panel
            {
                Size = new Size(140, 30),
                BackColor = Color.FromArgb(254, 207, 106) // accent muted
            };

            ApplyPanelStyle(SeeMorePanel);

            SeeMoreLabel = new Label
            {
                Text = "See more pets",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UiTheme.TextPrimary,
                AutoSize = true
            };

            SeeMorePanel.Controls.Add(SeeMoreLabel);

            SeeMoreLabel.Location = new Point(
                (SeeMorePanel.Width - SeeMoreLabel.Width) / 2,
                (SeeMorePanel.Height - SeeMoreLabel.Height) / 2
            );

            SeeMorePanel.Location = new Point(
                (RecentPetsPanel.Width - SeeMorePanel.Width) / 2,
                278
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
            if (e.Row == 0)
            {
                using var hb = new SolidBrush(UiTheme.TableHeaderBg);
                e.Graphics.FillRectangle(hb, e.CellBounds);
            }
            else if (e.Row % 2 == 0)
            {
                using var ab = new SolidBrush(UiTheme.TableRowAlt);
                e.Graphics.FillRectangle(ab, e.CellBounds);
            }
            if (e.Row > 0)
            {
                using var line = new Pen(UiTheme.TableGridLine, 1f);
                int y = e.CellBounds.Bottom - 1;
                e.Graphics.DrawLine(line, e.CellBounds.Left, y, e.CellBounds.Right, y);
            }
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

                Color lineColor = UiTheme.Primary;
                using (Pen linePen = new Pen(lineColor, 2.5f) { LineJoin = LineJoin.Round })
                    g.DrawLines(linePen, points);
                using (Brush dot = new SolidBrush(lineColor))
                using (Pen outline = new Pen(Color.White, 1.5f))
                {
                    foreach (var p in points)
                    {
                        g.FillEllipse(dot, p.X - 4, p.Y - 4, 8, 8);
                        g.DrawEllipse(outline, p.X - 4, p.Y - 4, 8, 8);
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
            p.BackColor = UiTheme.Surface;
            p.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = p.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using var path = RoundedRect(rect, radius);
                using var fill = new SolidBrush(UiTheme.Surface);
                using var border = new Pen(UiTheme.TableGridLine, 1f);
                e.Graphics.FillPath(fill, path);
                e.Graphics.DrawPath(border, path);
            };
        }

        // ================= SHADOW =================

        // ================= HEADER =================
        private void Header_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = Header.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using var brush = new LinearGradientBrush(rect, Color.FromArgb(45, 62, 145), UiTheme.Primary, LinearGradientMode.Vertical);
            using var path = RoundedRect(rect, 22);
            e.Graphics.FillPath(brush, path);
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
            }
        }
    }
}