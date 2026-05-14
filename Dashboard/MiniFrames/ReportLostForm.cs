using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class ReportLostForm : Form
    {
        public static Func<List<OwnerPetBridgeGroup>>? BridgeLoadOwnerPets;
        public static Func<string, string, DateTime, string, string, bool>? BridgeSaveLostReport;

        public ReportLostForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Report Lost Pet";
            this.Size = new Size(490, 520);
            this.MinimumSize = new Size(490, 520);
            this.MaximumSize = new Size(490, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            var groups = BridgeLoadOwnerPets?.Invoke() ?? new List<OwnerPetBridgeGroup>();

            var panelHeader = new Panel
            {
                Size = new Size(490, 68),
                Location = new Point(0, 0),
                BackColor = Color.White
            };
            panelHeader.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(10, 10, panelHeader.Width - 20, panelHeader.Height - 10);
                using (var path = RoundedPath(rect, 20))
                using (var brush = new LinearGradientBrush(
                    new Point(0, rect.Top), new Point(0, rect.Bottom),
                    Color.FromArgb(239, 68, 68),
                    Color.FromArgb(220, 38, 38)))
                    g.FillPath(brush, path);
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "🚨",
                Font = new Font("Inter", 18, FontStyle.Regular),
                ForeColor = Color.FromArgb(254, 202, 202),
                Location = new Point(32, 23),
                Size = new Size(34, 34),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "Report Lost Pet",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(76, 25),
                Size = new Size(310, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(34, 34),
                Location = new Point(424, 16),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);
            btnClose.Click += (s, e) => this.Close();
            panelHeader.Controls.Add(btnClose);

            var scroll = new Panel
            {
                Location = new Point(0, 68),
                Size = new Size(490, 392),
                AutoScroll = true,
                BackColor = Color.White
            };
            this.Controls.Add(scroll);

            Label MakeLabel(string text, int y) => new Label
            {
                Text = text,
                Font = new Font("Inter", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(24, y),
                AutoSize = true,
                BackColor = Color.White
            };

            (Panel w, ComboBox cb) MakeComboBox(int y, string hint)
            {
                int radius = 20;
                var w = new Panel
                {
                    Location = new Point(24, y),
                    Size = new Size(440, 38),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                w.Region = RoundedRegion(w.Size, radius);
                w.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, w.Width - 1, w.Height - 1), radius))
                        g.DrawPath(pen, path);
                };
                var cb = new ComboBox
                {
                    Location = new Point(10, 6),
                    Size = new Size(w.Width - 20, 26),
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    FlatStyle = FlatStyle.Flat,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cb.Items.Add(hint);
                cb.SelectedIndex = 0;
                w.Controls.Add(cb);
                return (w, cb);
            }

            (Panel w, TextBox tb) MakeTextBox(int y, int height, string placeholder,
                bool multiline = false, float fontSize = 12f)
            {
                int radius = 20;
                var w = new Panel
                {
                    Location = new Point(24, y),
                    Size = new Size(440, height + 14),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                w.Region = RoundedRegion(w.Size, radius);
                w.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, w.Width - 1, w.Height - 1), radius))
                        g.DrawPath(pen, path);
                };
                var tb = new TextBox
                {
                    Location = new Point(12, 0),
                    Size = new Size(w.Width - 24, height),
                    Font = new Font("Inter", fontSize, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    BorderStyle = BorderStyle.None,
                    Multiline = multiline,
                    WordWrap = multiline,
                    ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None,
                    Text = placeholder
                };
                tb.Location = new Point(12, multiline ? 8 : (w.Height - tb.PreferredHeight) / 2);
                tb.Enter += (s, e) =>
                {
                    if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(30, 41, 59); }
                };
                tb.Leave += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(100, 116, 139); }
                };
                w.Controls.Add(tb);
                return (w, tb);
            }

            (Panel w, DateTimePicker dtp) MakeDatePicker(int y)
            {
                int radius = 20;
                var w = new Panel
                {
                    Location = new Point(24, y),
                    Size = new Size(440, 38),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                w.Region = RoundedRegion(w.Size, radius);
                w.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, w.Width - 1, w.Height - 1), radius))
                        g.DrawPath(pen, path);
                };
                var dtp = new DateTimePicker
                {
                    Location = new Point(10, 6),
                    Size = new Size(w.Width - 20, 26),
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    Format = DateTimePickerFormat.Short,
                    Value = DateTime.Today
                };
                w.Controls.Add(dtp);
                return (w, dtp);
            }

            scroll.Controls.Add(MakeLabel("Owner", 10));
            var (ownerW, cbOwner) = MakeComboBox(32, "— Select Owner —");
            foreach (var g in groups)
                cbOwner.Items.Add(g.DisplayLabel);
            scroll.Controls.Add(ownerW);

            scroll.Controls.Add(MakeLabel("Pet", 82));
            var (petW, cbPet) = MakeComboBox(104, "— Select Owner First —");
            cbPet.Enabled = false;
            scroll.Controls.Add(petW);

            cbOwner.SelectedIndexChanged += (s, e) =>
            {
                cbPet.Items.Clear();
                int oi = cbOwner.SelectedIndex - 1;
                if (cbOwner.SelectedIndex <= 0 || oi < 0 || oi >= groups.Count)
                {
                    cbPet.Items.Add("— Select Owner First —");
                    cbPet.SelectedIndex = 0;
                    cbPet.Enabled = false;
                    cbPet.ForeColor = Color.FromArgb(100, 116, 139);
                }
                else
                {
                    var grp = groups[oi];
                    cbPet.Enabled = true;
                    cbPet.ForeColor = Color.FromArgb(30, 41, 59);
                    cbPet.Items.Add(new PetBridgeOption { PetId = 0, Name = "— Select Pet —" });
                    foreach (var p in grp.Pets)
                        cbPet.Items.Add(p);
                    cbPet.SelectedIndex = 0;
                }
            };

            scroll.Controls.Add(MakeLabel("Lost Date", 154));
            var (lostDateW, dtpLost) = MakeDatePicker(176);
            scroll.Controls.Add(lostDateW);

            scroll.Controls.Add(MakeLabel("Last Seen Location", 226));
            var (locW, tbLoc) = MakeTextBox(248, 24, "Barangay Park, near market...");
            scroll.Controls.Add(locW);

            scroll.Controls.Add(MakeLabel("Pet Description", 300));
            var (descW, tbDesc) = MakeTextBox(322, 100,
                "Color, collar, behavior, reward...",
                multiline: true, fontSize: 11f);
            scroll.Controls.Add(descW);

            var panelButtons = new Panel
            {
                Location = new Point(0, 462),
                Size = new Size(490, 58),
                BackColor = Color.White
            };
            this.Controls.Add(panelButtons);

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(130, 42),
                Location = new Point(74, 8),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(75, 85, 99),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
            btnCancel.Region = RoundedRegion(btnCancel.Size, 20);
            btnCancel.Click += (s, e) => this.Close();
            panelButtons.Controls.Add(btnCancel);

            var btnReport = new Button
            {
                Text = "🚨 Report Lost",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(195, 42),
                Location = new Point(220, 8),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 38, 38),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnReport.FlatAppearance.BorderSize = 0;
            btnReport.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 28, 28);
            btnReport.Region = RoundedRegion(btnReport.Size, 20);
            panelButtons.Controls.Add(btnReport);

            btnReport.Click += (_, _) =>
            {
                int oi = cbOwner.SelectedIndex - 1;
                if (cbOwner.SelectedIndex <= 0 || oi < 0 || oi >= groups.Count)
                {
                    MessageBox.Show("Select an owner.", "Report Lost", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ownerLine = groups[oi].DisplayLabel;
                if (cbPet.SelectedItem is not PetBridgeOption petOpt || petOpt.PetId <= 0)
                {
                    MessageBox.Show("Select a pet.", "Report Lost", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string loc = tbLoc.Text?.Trim() ?? "";
                if (loc == "Barangay Park, near market...")
                    loc = "";

                string desc = tbDesc.Text?.Trim() ?? "";
                if (desc == "Color, collar, behavior, reward...")
                    desc = "";

                var bridge = BridgeSaveLostReport;
                if (bridge == null)
                {
                    MessageBox.Show("Save handler is not connected.", "Report Lost", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var lostAt = dtpLost.Value.Date.Add(DateTime.Now.TimeOfDay);
                if (bridge(ownerLine, petOpt.Name, lostAt, loc, desc))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(76, 476, 130, 42), 20))
                    g.FillPath(brush, path);

                using (var brush = new SolidBrush(Color.FromArgb(50, 220, 38, 38)))
                using (var path = RoundedPath(new Rectangle(222, 476, 195, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        private static Region RoundedRegion(Size size, int radius)
        {
            return new Region(RoundedPath(new Rectangle(0, 0, size.Width, size.Height), radius));
        }

        private static GraphicsPath RoundedPath(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
