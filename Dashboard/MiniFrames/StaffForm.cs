using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class StaffForm : Form
    {
        public static Func<string, string, string, string, string, string, bool>? BridgeSaveStaff;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private TextBox tbUsername;
        private TextBox tbFullName;
        private ComboBox cbRole;
        private TextBox tbPhone;
        private TextBox tbEmail;
        private TextBox tbAtPin;

        public StaffForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "New Staff Account";
            this.Size = new Size(490, 520);
            this.MinimumSize = new Size(490, 520);
            this.MaximumSize = new Size(490, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(237, 237, 237);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            // Scroll Panel 

            var scrollPanel = new Panel
            {
                Size = new Size(490, 520),
                Location = new Point(0, 0),
                AutoScroll = true,
                BackColor = Color.FromArgb(237, 237, 237)
            };
            scrollPanel.HorizontalScroll.Visible = false;
            scrollPanel.HorizontalScroll.Enabled = false;
            this.Controls.Add(scrollPanel);

            // Header 

            var panelHeader = new Panel
            {
                Size = new Size(470, 80),
                Location = new Point(10, 0),
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
                           Color.FromArgb(59, 130, 246),
                           Color.FromArgb(30, 64, 175)))
                    g.FillPath(brush, path);
            };
            scrollPanel.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "👥",
                Font = new Font("Segoe UI Emoji", 20),
                ForeColor = Color.FromArgb(147, 197, 253),
                Location = new Point(32, 26),
                Size = new Size(36, 36),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "New Staff Account",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(76, 28),
                Size = new Size(290, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 14, FontStyle.Bold),
                Size = new Size(38, 38),
                Location = new Point(400, 21),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);
            btnClose.Click += (s, e) => this.Close();
            panelHeader.Controls.Add(btnClose);

            panelHeader.MouseDown += DragForm;
            lblTitle.MouseDown += DragForm;
            lblIcon.MouseDown += DragForm;

            // Helpers

            Label MakeLabel(string text, int y, bool muted = false) => new Label
            {
                Text = text,
                Font = new Font("Inter", 12, muted ? FontStyle.Italic : FontStyle.Bold),
                ForeColor = muted ? Color.FromArgb(156, 163, 175) : Color.FromArgb(55, 65, 81),
                Location = new Point(28, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            (Panel w, TextBox tb) MakeTextBox(int y, string placeholder, bool readOnly = false)
            {
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(430, 38),
                    BackColor = readOnly ? Color.FromArgb(243, 244, 246) : Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, 20);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    var penColor = readOnly ? Color.FromArgb(209, 213, 219) : Color.FromArgb(125, 211, 252);
                    using (var pen = new Pen(penColor, 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), 20))
                        g.DrawPath(pen, path);
                };
                var tb = new TextBox
                {
                    Size = new Size(wrapper.Width - 24, 22),
                    Font = new Font("Inter", 12),
                    BackColor = readOnly ? Color.FromArgb(243, 244, 246) : Color.FromArgb(241, 245, 249),
                    ForeColor = readOnly ? Color.FromArgb(59, 130, 246) : Color.FromArgb(100, 116, 139),
                    BorderStyle = BorderStyle.None,
                    ReadOnly = readOnly,
                    Text = placeholder
                };
                tb.Location = new Point(12, (wrapper.Height - tb.PreferredHeight) / 2);
                if (!readOnly)
                {
                    tb.Enter += (s, e) => { if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(30, 41, 59); } };
                    tb.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(100, 116, 139); } };
                }
                wrapper.Controls.Add(tb);
                return (wrapper, tb);
            }

            (Panel w, ComboBox cb) MakeComboBox(int y, string[] items)
            {
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(430, 38),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, 20);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), 20))
                        g.DrawPath(pen, path);
                };
                var cb = new ComboBox
                {
                    Font = new Font("Inter", 12),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    FlatStyle = FlatStyle.Flat,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Size = new Size(wrapper.Width - 24, 24)
                };
                cb.Items.AddRange(items);
                cb.SelectedIndex = 0;
                cb.Location = new Point(12, (wrapper.Height - cb.PreferredHeight) / 2);
                wrapper.Controls.Add(cb);
                return (wrapper, cb);
            }

            // Fields 

            scrollPanel.Controls.Add(MakeLabel("Username", 90));
            var (wUsername, _tb1) = MakeTextBox(112, "e.g. staff3");
            tbUsername = _tb1;
            scrollPanel.Controls.Add(wUsername);

            scrollPanel.Controls.Add(MakeLabel("Full Name", 158));
            var (wFullName, _tb2) = MakeTextBox(180, "e.g. Jussa Necole");
            tbFullName = _tb2;
            scrollPanel.Controls.Add(wFullName);

            scrollPanel.Controls.Add(MakeLabel("Role", 226));
            var (wRole, _cb1) = MakeComboBox(248, new[] { "Staff", "Admin" });
            cbRole = _cb1;
            scrollPanel.Controls.Add(wRole);

            scrollPanel.Controls.Add(MakeLabel("📞 Phone", 294));
            var (wPhone, phoneTb) = MakeTextBox(316, "+63 912 345 6789");
            tbPhone = phoneTb;
            scrollPanel.Controls.Add(wPhone);

            scrollPanel.Controls.Add(MakeLabel("📧 Email", 362));
            var (wEmail, _tb3) = MakeTextBox(384, "staff3@pawtech.ph");
            tbEmail = _tb3;
            scrollPanel.Controls.Add(wEmail);

            scrollPanel.Controls.Add(MakeLabel("🔐 AT-PIN", 430, muted: true));
            var (wPin, _tb4) = MakeTextBox(452, "e.g. AT789");
            tbAtPin = _tb4;
            scrollPanel.Controls.Add(wPin);

            // Buttons 

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(140, 42),
                Location = new Point(55, 506),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 85, 99);
            btnCancel.Region = RoundedRegion(btnCancel.Size, 20);
            btnCancel.Click += (s, e) => this.Close();
            scrollPanel.Controls.Add(btnCancel);

            var btnCreate = new Button
            {
                Text = "👥 Create Staff",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(200, 42),
                Location = new Point(235, 506),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnCreate.Region = RoundedRegion(btnCreate.Size, 20);
            btnCreate.Click += OnCreateStaff;
            scrollPanel.Controls.Add(btnCreate);

            // Shadows

            scrollPanel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(57, 508, 140, 42), 20))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(50, 59, 130, 246)))
                using (var path = RoundedPath(new Rectangle(237, 508, 200, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        //Create Staff Handler 

        private void OnCreateStaff(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) || tbUsername.Text == "e.g. staff3")
            {
                MessageBox.Show("Please enter a username.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbFullName.Text) || tbFullName.Text == "e.g. Jussa Necole")
            {
                MessageBox.Show("Please enter the full name.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string phone = tbPhone.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(phone) || phone == "+63 912 345 6789")
            {
                MessageBox.Show("Please enter a phone number.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbAtPin.Text) || tbAtPin.Text == "e.g. AT789")
            {
                MessageBox.Show("Please enter the AT-PIN.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string email = tbEmail.Text?.Trim() ?? "";
            if (email == "staff3@pawtech.ph")
                email = "";

            var bridge = BridgeSaveStaff;
            if (bridge != null)
            {
                if (!bridge(tbUsername.Text.Trim(), tbFullName.Text.Trim(), cbRole.Text, phone, email, tbAtPin.Text.Trim()))
                    return;
            }

            MessageBox.Show(
                $"Staff account created.\n\n" +
                $"Username : {tbUsername.Text}\n" +
                $"Name     : {tbFullName.Text}\n" +
                $"Role     : {cbRole.Text}\n" +
                $"Phone    : {phone}\n" +
                $"AT-PIN   : {tbAtPin.Text}",
                "Staff Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        // Drag 

        private void DragForm(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Shadow Helper 

        private static void DrawShadow(Graphics g, Rectangle rect, int radius, Color color, int offset)
        {
            for (int i = offset; i > 0; i--)
            {
                int alpha = (int)(color.A * ((float)i / offset) * 0.4f);
                var inflated = Rectangle.Inflate(rect, i, i);
                inflated.Offset(0, i / 2);
                using (var brush = new SolidBrush(Color.FromArgb(alpha, color.R, color.G, color.B)))
                using (var path = RoundedPath(inflated, radius + i))
                    g.FillPath(brush, path);
            }
        }

        // Rounded Helpers 

        private static Region RoundedRegion(Size size, int radius) =>
            new Region(RoundedPath(new Rectangle(0, 0, size.Width, size.Height), radius));

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

        private static GraphicsPath RoundedPathTopOnly(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            path.CloseFigure();
            return path;
        }
    }
}