using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class LostPetDescriptionForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private readonly LostPetReport _report;

        public LostPetDescriptionForm(LostPetReport report)
        {
            _report = report;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = $"Lost Report - {_report.PetName}";
            this.Size = new Size(490, 450);
            this.MinimumSize = new Size(490, 450);
            this.MaximumSize = new Size(490, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(237, 237, 237);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

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
                    Color.FromArgb(239, 68, 68),
                    Color.FromArgb(220, 38, 38)))
                    g.FillPath(brush, path);
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "🚨",
                Font = new Font("Segoe UI Emoji", 20),
                ForeColor = Color.FromArgb(254, 202, 202),
                Location = new Point(32, 22),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"Lost Report: {_report.PetName}",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(80, 28),
                Size = new Size(300, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(38, 38),
                Location = new Point(410, 21),
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

            panelHeader.MouseDown += DragForm;
            lblTitle.MouseDown += DragForm;
            lblIcon.MouseDown += DragForm;

            // Info Card 

            var cardInfo = new Panel
            {
                Size = new Size(440, 100),
                Location = new Point(25, 94),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardInfo.Region = RoundedRegion(cardInfo.Size, 16);
            cardInfo.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, cardInfo.Width - 1, cardInfo.Height - 1), 16))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(cardInfo);

            var infoRows = new[]
            {
                ("Owner:",    _report.OwnerName),
                ("Pet:",      _report.PetName),
                ("Date Lost:", _report.DateLost.ToString("MMMM dd, yyyy")),
                ("Last Seen:", _report.LastSeenLocation)
            };

            int rowY = 10;
            foreach (var (label, value) in infoRows)
            {
                cardInfo.Controls.Add(new Label
                {
                    Text = label,
                    Font = new Font("Inter", 11, FontStyle.Regular),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    Location = new Point(20, rowY),
                    AutoSize = true,
                    BackColor = Color.Transparent
                });
                cardInfo.Controls.Add(new Label
                {
                    Text = value,
                    Font = new Font("Inter", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Location = new Point(110, rowY),
                    Size = new Size(300, 20),
                    BackColor = Color.Transparent
                });
                rowY += 22;
            }

            //  Description Card

            this.Controls.Add(new Label
            {
                Text = "Pet Description",
                Font = new Font("Inter", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(25, 206),
                AutoSize = true,
                BackColor = Color.Transparent
            });

            var cardDesc = new Panel
            {
                Size = new Size(440, 120),
                Location = new Point(25, 228),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardDesc.Region = RoundedRegion(cardDesc.Size, 16);
            cardDesc.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, cardDesc.Width - 1, cardDesc.Height - 1), 16))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(cardDesc);

            var lblDesc = new Label
            {
                Text = string.IsNullOrEmpty(_report.Description)
                    ? "No description provided."
                    : _report.Description,
                Font = new Font("Inter", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(16, 12),
                Size = new Size(408, 96),
                BackColor = Color.Transparent
            };
            cardDesc.Controls.Add(lblDesc);

            // Action Buttons

            var btnEdit = new Button
            {
                Text = "✏️ Edit Report",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(200, 42),
                Location = new Point(25, 370),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 158, 11),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 119, 6);
            btnEdit.Region = RoundedRegion(btnEdit.Size, 12);
            btnEdit.Click += (s, e) =>
            {
                var editForm = new EditReportLostForm(_report);
                editForm.ShowDialog(this);
            };
            this.Controls.Add(btnEdit);

            var btnClose2 = new Button
            {
                Text = "Close",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(200, 42),
                Location = new Point(265, 370),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(107, 114, 128),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnClose2.FlatAppearance.BorderSize = 0;
            btnClose2.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 85, 99);
            btnClose2.Region = RoundedRegion(btnClose2.Size, 12);
            btnClose2.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose2);

            // Shadows

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(40, 245, 158, 11)))
                using (var path = RoundedPath(new Rectangle(27, 372, 200, 42), 12))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(40, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(267, 372, 200, 42), 12))
                    g.FillPath(brush, path);
            };
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