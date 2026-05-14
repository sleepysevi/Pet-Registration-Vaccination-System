using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class EditOwnerForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            int lParam
        );

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private readonly Owner _editingOwner;

        private TextBox tbName;
        private TextBox tbPhone;
        private TextBox tbAddress;
        private TextBox tbEmail;
        private TextBox tbNotes;
        public Owner? UpdatedOwner { get; private set; }

        public EditOwnerForm(Owner editingOwner)
        {
            _editingOwner = editingOwner;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Edit Owner";
            this.Size = new Size(500, 520);
            this.MinimumSize = new Size(500, 520);
            this.MaximumSize = new Size(500, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(237, 237, 237);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            // Header Panel 

            var panelHeader = new Panel
            {
                Size = new Size(480, 80),
                Location = new Point(10, 0),
                BackColor = Color.Transparent
            };
            panelHeader.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(
                    10, 10,
                    panelHeader.Width - 20,
                    panelHeader.Height - 10
                );
                using (var path = RoundedPath(rect, 20))
                using (var brush = new LinearGradientBrush(
                           new Point(0, rect.Top),
                           new Point(0, rect.Bottom),
                           Color.FromArgb(59, 130, 246),
                           Color.FromArgb(30, 64, 175)))
                    g.FillPath(brush, path);
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "👤",
                Font = new Font("Inter", 20, FontStyle.Regular),
                ForeColor = Color.FromArgb(147, 197, 253),
                Location = new Point(32, 26),
                Size = new Size(36, 36),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"Edit Owner: {_editingOwner.Name}",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(76, 28),
                Size = new Size(340, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(38, 38),
                Location = new Point(432, 24),
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

            // Field Helpers

            Label MakeLabel(string text, int y) => new Label
            {
                Text = text,
                Font = new Font("Inter", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(28, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            (Panel w, TextBox tb) MakeTextBox(
                int y,
                int height,
                string value,
                bool multiline = false,
                float fontSize = 14f)
            {
                int radius = 20;
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(440, height + 14),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, radius);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(
                               new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1),
                               radius))
                        g.DrawPath(pen, path);
                };
                var tb = new TextBox
                {
                    Size = new Size(wrapper.Width - 24, height),
                    Font = new Font("Inter", fontSize, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    BorderStyle = BorderStyle.None,
                    Multiline = multiline,
                    WordWrap = multiline,
                    ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None,
                    Text = value
                };
                tb.Location = new Point(
                    12,
                    multiline ? 8 : (wrapper.Height - tb.PreferredHeight) / 2
                );
                wrapper.Controls.Add(tb);
                return (wrapper, tb);
            }

            //  Fields (pre-filled) 

            this.Controls.Add(MakeLabel("Full Name", 96));
            var (wName, _tb1) = MakeTextBox(118, 24, _editingOwner.Name, fontSize: 12f);
            tbName = _tb1;
            this.Controls.Add(wName);

            this.Controls.Add(MakeLabel("Contact Number", 155));
            var (wPhone, _tb2) = MakeTextBox(177, 24, _editingOwner.ContactNumber, fontSize: 12f);
            tbPhone = _tb2;
            this.Controls.Add(wPhone);

            this.Controls.Add(MakeLabel("Address", 217));
            var (wAddress, _tb3) = MakeTextBox(239, 44, _editingOwner.Address, multiline: true, fontSize: 12f);
            tbAddress = _tb3;
            this.Controls.Add(wAddress);

            this.Controls.Add(MakeLabel("Email (Optional)", 303));
            var (wEmail, _tb4) = MakeTextBox(325, 24, _editingOwner.Email, fontSize: 12f);
            tbEmail = _tb4;
            this.Controls.Add(wEmail);

            this.Controls.Add(MakeLabel("Notes (Optional)", 365));
            var (wNotes, _tb5) = MakeTextBox(387, 44, _editingOwner.Notes, multiline: true, fontSize: 12f);
            tbNotes = _tb5;
            this.Controls.Add(wNotes);

            //  Buttons 

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(110, 42),
                Location = new Point(102, 460),
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
            this.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Text = "Update Owner",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(165, 42),
                Location = new Point(232, 460),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnSave.Region = RoundedRegion(btnSave.Size, 20);
            btnSave.Click += OnUpdateOwner;
            this.Controls.Add(btnSave);

            // Shadows

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(104, 462, 110, 42), 20))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(50, 59, 130, 246)))
                using (var path = RoundedPath(new Rectangle(234, 462, 165, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        // Update Handler 

        private void OnUpdateOwner(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Please enter the owner's name.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Please enter a contact number.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UpdatedOwner = new Owner
            {
                OwnerID = _editingOwner.OwnerID,
                Name = tbName.Text.Trim(),
                ContactNumber = tbPhone.Text.Trim(),
                Address = tbAddress.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                Notes = tbNotes.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        // Drag 

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
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
    }
}