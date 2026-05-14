using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class ARShotForm : Form
    {
        public static Func<List<OwnerPetBridgeGroup>>? BridgeLoadOwnerPets;
        public static Func<int, DateTime, DateTime?, string, bool>? BridgeSaveVaccination;

        public ARShotForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "New Anti-Rabies Shot";
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
                Size = new Size(480, 90),
                Location = new Point(5, 0),
                BackColor = Color.Transparent
            };
            panelHeader.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(10, 10, panelHeader.Width - 20, panelHeader.Height - 10);
                using (var path = RoundedPath(rect, 20))
                using (var brush = new LinearGradientBrush(
                    new Point(0, rect.Top), new Point(0, rect.Bottom),
                    Color.FromArgb(16, 185, 129),
                    Color.FromArgb(5, 150, 105)))
                {
                    g.FillPath(brush, path);
                }
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "💉",
                Font = new Font("Inter", 22, FontStyle.Regular),
                ForeColor = Color.FromArgb(167, 243, 208),
                Location = new Point(32, 28),
                Size = new Size(38, 38),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "New Anti-Rabies Shot",
                Font = new Font("Inter", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(80, 32),
                Size = new Size(330, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(38, 38),
                Location = new Point(422, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);
            btnClose.Click += (_, _) => Close();
            panelHeader.Controls.Add(btnClose);

            Label MakeLabel(string text, int y) => new Label
            {
                Text = text,
                Font = new Font("Inter", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(23, y),
                AutoSize = true,
                BackColor = Color.White
            };

            (Panel wrapper, ComboBox cb) MakeComboBox(int y, string hint)
            {
                int radius = 20;
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(440, 38),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, radius);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), radius))
                        g.DrawPath(pen, path);
                };

                var cb = new ComboBox
                {
                    Location = new Point(10, 6),
                    Size = new Size(wrapper.Width - 20, 26),
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    FlatStyle = FlatStyle.Flat,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cb.Items.Add(hint);
                cb.SelectedIndex = 0;
                wrapper.Controls.Add(cb);
                return (wrapper, cb);
            }

            (Panel wrapper, TextBox tb) MakeTextBox(int y, int height, string placeholder, bool multiline = false, float fontSize = 12f)
            {
                int radius = 20;
                var wrapper = new Panel
                {
                    Location = new Point(23, y),
                    Size = new Size(440, height + 14),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, radius);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), radius))
                        g.DrawPath(pen, path);
                };

                var tb = new TextBox
                {
                    Location = new Point(12, 0),
                    Size = new Size(wrapper.Width - 24, height),
                    Font = new Font("Inter", fontSize, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    BorderStyle = BorderStyle.None,
                    Multiline = multiline,
                    WordWrap = multiline,
                    ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None,
                    Text = placeholder
                };
                tb.Location = new Point(12, multiline ? 8 : (wrapper.Height - tb.PreferredHeight) / 2);

                tb.Enter += (s, e) =>
                {
                    if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(30, 41, 59); }
                };
                tb.Leave += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(100, 116, 139); }
                };

                wrapper.Controls.Add(tb);
                return (wrapper, tb);
            }

            this.Controls.Add(MakeLabel("Owner", 100));
            var (ownerWrapper, cbOwner) = MakeComboBox(122, "— Select Owner —");
            foreach (var g in groups)
                cbOwner.Items.Add(g.DisplayLabel);
            this.Controls.Add(ownerWrapper);

            this.Controls.Add(MakeLabel("Pet", 172));
            var (petWrapper, cbPet) = MakeComboBox(194, "— Select Owner First —");
            cbPet.Enabled = false;
            this.Controls.Add(petWrapper);

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

            this.Controls.Add(MakeLabel("Date Given", 244));
            var (wrapDate, tbDate) = MakeTextBox(266, 24, DateTime.Today.ToString("yyyy-MM-dd"), fontSize: 12f);
            this.Controls.Add(wrapDate);

            this.Controls.Add(MakeLabel("Next Due", 306));
            var (wrapNext, tbNext) = MakeTextBox(328, 24, "(optional) yyyy-MM-dd", fontSize: 12f);
            this.Controls.Add(wrapNext);

            this.Controls.Add(MakeLabel("Notes", 368));
            var (wrapNotes, tbNotes) = MakeTextBox(390, 54, "Notes (optional)", multiline: true, fontSize: 12f);
            this.Controls.Add(wrapNotes);

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(110, 42),
                Location = new Point(102, 462),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(75, 85, 99),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 65, 81);
            btnCancel.Region = RoundedRegion(btnCancel.Size, 20);
            btnCancel.Click += (_, _) => Close();
            this.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Text = "💉 Record Shot",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(175, 42),
                Location = new Point(222, 462),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(4, 120, 87),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(3, 100, 72);
            btnSave.Region = RoundedRegion(btnSave.Size, 20);
            this.Controls.Add(btnSave);

            btnSave.Click += (_, _) =>
            {
                if (cbPet.SelectedItem is not PetBridgeOption opt || opt.PetId <= 0)
                {
                    MessageBox.Show("Select a pet.", "Anti-Rabies Shot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string dText = tbDate.Text?.Trim() ?? "";
                if (!DateTime.TryParse(dText, out var given))
                {
                    MessageBox.Show("Enter a valid date given (yyyy-MM-dd).", "Anti-Rabies Shot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime? nextDue = null;
                string nText = tbNext.Text?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(nText) && !nText.StartsWith("(optional)", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(nText, out var nd))
                        nextDue = nd;
                }

                string notes = tbNotes.Text?.Trim() ?? "";
                if (notes == "Notes (optional)")
                    notes = "";

                var bridge = BridgeSaveVaccination;
                if (bridge == null)
                {
                    MessageBox.Show("Save handler is not connected.", "Anti-Rabies Shot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (bridge(opt.PetId, given, nextDue, notes))
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
                using (var path = RoundedPath(new Rectangle(104, 464, 110, 42), 20))
                    g.FillPath(brush, path);

                using (var brush = new SolidBrush(Color.FromArgb(50, 4, 120, 87)))
                using (var path = RoundedPath(new Rectangle(224, 464, 175, 42), 20))
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
