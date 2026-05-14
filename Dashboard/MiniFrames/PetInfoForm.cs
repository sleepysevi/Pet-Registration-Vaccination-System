using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class PetInfoForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private readonly Pet _pet;
        private readonly Owner _owner;

        public PetInfoForm(Pet pet, Owner owner)
        {
            _pet = pet;
            _owner = owner;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = $"{_pet.Name} - Pet Profile";
            this.Size = new Size(490, 620);
            this.MinimumSize = new Size(490, 620);
            this.MaximumSize = new Size(490, 620);
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
                    Color.FromArgb(16, 185, 129),
                    Color.FromArgb(5, 150, 105)))
                    g.FillPath(brush, path);
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = _pet.Species switch
                {
                    "Cat" => "🐈",
                    "Bird" => "🐦",
                    _ => "🐕"
                },
                Font = new Font("Segoe UI Emoji", 20),
                ForeColor = Color.FromArgb(134, 239, 172),
                Location = new Point(32, 22),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"{_pet.Name} ({_pet.Breed})",
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
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);
            btnClose.Click += (s, e) => this.Close();
            panelHeader.Controls.Add(btnClose);

            panelHeader.MouseDown += DragForm;
            lblTitle.MouseDown += DragForm;
            lblIcon.MouseDown += DragForm;

            // Photo Card 

            var cardPhoto = new Panel
            {
                Size = new Size(440, 160),
                Location = new Point(25, 95),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardPhoto.Region = RoundedRegion(cardPhoto.Size, 20);
            cardPhoto.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, cardPhoto.Width - 1, cardPhoto.Height - 1), 20))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(cardPhoto);

            var pbPhoto = new PictureBox
            {
                Size = new Size(130, 130),
                Location = new Point((cardPhoto.Width - 130) / 2, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(226, 232, 240)
            };
            var circlePath = new GraphicsPath();
            circlePath.AddEllipse(0, 0, pbPhoto.Width, pbPhoto.Height);
            pbPhoto.Region = new Region(circlePath);

            if (!string.IsNullOrEmpty(_pet.PhotoPath) &&
                System.IO.File.Exists(_pet.PhotoPath))
                pbPhoto.Image = Image.FromFile(_pet.PhotoPath);

            pbPhoto.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (pbPhoto.Image == null)
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(203, 213, 225)),
                        0, 0, pbPhoto.Width, pbPhoto.Height);
                    g.DrawString(
                        _pet.Species switch { "Cat" => "🐈", "Bird" => "🐦", _ => "🐕" },
                        new Font("Segoe UI Emoji", 36),
                        new SolidBrush(Color.FromArgb(148, 163, 184)),
                        new RectangleF(0, 0, pbPhoto.Width, pbPhoto.Height),
                        new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                }
                using (var pen = new Pen(Color.FromArgb(209, 213, 219), 3f))
                    g.DrawEllipse(pen, 1, 1, pbPhoto.Width - 3, pbPhoto.Height - 3);
            };
            cardPhoto.Controls.Add(pbPhoto);

            // Basic Info Grid 

            var cardInfo = new Panel
            {
                Size = new Size(440, 130),
                Location = new Point(25, 270),
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
                ("Name:",  _pet.Name,              "Age:",   $"{_pet.Age} yrs"),
                ("Breed:", _pet.Breed,             "Color:", _pet.Color),
                ("Owner:", _owner.Name,        "Phone:", _owner.ContactNumber)
            };

            int rowY = 14;
            foreach (var (lbl1, val1, lbl2, val2) in infoRows)
            {
                // Left label
                cardInfo.Controls.Add(new Label
                {
                    Text = lbl1,
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    Location = new Point(20, rowY),
                    AutoSize = true,
                    BackColor = Color.Transparent
                });
                // Left value
                cardInfo.Controls.Add(new Label
                {
                    Text = val1,
                    Font = new Font("Inter", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Location = new Point(80, rowY),
                    AutoSize = true,
                    BackColor = Color.Transparent
                });
                // Right label
                cardInfo.Controls.Add(new Label
                {
                    Text = lbl2,
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    ForeColor = Color.FromArgb(107, 114, 128),
                    Location = new Point(240, rowY),
                    AutoSize = true,
                    BackColor = Color.Transparent
                });
                // Right value
                cardInfo.Controls.Add(new Label
                {
                    Text = val2,
                    Font = new Font("Inter", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Location = new Point(300, rowY),
                    AutoSize = true,
                    BackColor = Color.Transparent
                });
                rowY += 34;
            }

            // Status Cards

            var cardStatus = new Panel
            {
                Size = new Size(440, 100),
                Location = new Point(25, 415),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardStatus.Region = RoundedRegion(cardStatus.Size, 16);
            cardStatus.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, cardStatus.Width - 1, cardStatus.Height - 1), 16))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(cardStatus);

            // AR Vaccine row
            bool hasVaccine = !string.IsNullOrEmpty(_pet.LastVaccineDate);
            var lblVaccineIcon = new Label
            {
                Text = hasVaccine ? "🟢" : "🔴",
                Font = new Font("Segoe UI Emoji", 13),
                Location = new Point(20, 14),
                Size = new Size(28, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblVaccineIcon);

            var lblVaccineLabel = new Label
            {
                Text = "AR Vaccine:",
                Font = new Font("Inter", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 16),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblVaccineLabel);

            var lblVaccineVal = new Label
            {
                Text = hasVaccine
                    ? $"{_pet.LastVaccineDate}  ({_pet.VaccineDaysLeft} days left)"
                    : "Never vaccinated",
                Font = new Font("Inter", 12, FontStyle.Bold),
                ForeColor = hasVaccine
                    ? Color.FromArgb(16, 185, 129)
                    : Color.FromArgb(239, 68, 68),
                Location = new Point(148, 16),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblVaccineVal);

            // Divider
            cardStatus.Paint += (s, e) =>
            {
                e.Graphics.DrawLine(
                    new Pen(Color.FromArgb(226, 232, 240), 1f),
                    20, 52, cardStatus.Width - 20, 52);
            };

            // Lost Report row
            bool hasLostReport = !string.IsNullOrEmpty(_pet.LostReportDate);
            var lblLostIcon = new Label
            {
                Text = hasLostReport ? "🔴" : "🟢",
                Font = new Font("Segoe UI Emoji", 13),
                Location = new Point(20, 60),
                Size = new Size(28, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblLostIcon);

            var lblLostLabel = new Label
            {
                Text = "Lost Report:",
                Font = new Font("Inter", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 62),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblLostLabel);

            var lblLostVal = new Label
            {
                Text = hasLostReport
                    ? $"{_pet.LostReportDate} — {_pet.LostReportLocation}"
                    : "No active lost report",
                Font = new Font("Inter", 12, FontStyle.Bold),
                ForeColor = hasLostReport
                    ? Color.FromArgb(239, 68, 68)
                    : Color.FromArgb(16, 185, 129),
                Location = new Point(148, 62),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            cardStatus.Controls.Add(lblLostVal);

            // Action Buttons 

            var panelActions = new Panel
            {
                Size = new Size(440, 62),
                Location = new Point(25, 530),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            panelActions.Region = RoundedRegion(panelActions.Size, 14);
            panelActions.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, panelActions.Width - 1, panelActions.Height - 1), 14))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(panelActions);

            var actionButtons = new[]
            {
                ("✏️ Edit",   Color.FromArgb(59, 130, 246),  Color.FromArgb(37, 99, 235)),
                ("💉 AR Shot", Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105)),
                ("🚨 Lost",   Color.FromArgb(220, 38, 38),   Color.FromArgb(185, 28, 28)),
                ("📄 QR",     Color.FromArgb(245, 158, 11),  Color.FromArgb(217, 119, 6))
            };

            int btnX = 10;
            foreach (var (text, bg, hover) in actionButtons)
            {
                var btn = new Button
                {
                    Text = text,
                    Font = new Font("Inter", 10, FontStyle.Bold),
                    Size = new Size(98, 42),
                    Location = new Point(btnX, 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = bg,
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand,
                    TabStop = false
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = hover;
                btn.Region = RoundedRegion(btn.Size, 12);

                var capturedText = text;
                btn.Click += (s, e) =>
                {
                    if (capturedText.Contains("Edit"))
                    {
                        var editForm = new EditPetForm(_pet);
                        editForm.ShowDialog(this);
                    }
                    else if (capturedText.Contains("AR"))
                    {
                        var arForm = new ARShotForm();
                        arForm.ShowDialog(this);
                    }
                    else if (capturedText.Contains("Lost"))
                    {
                        var lostForm = new ReportLostForm();
                        lostForm.ShowDialog(this);
                    }
                    else if (capturedText.Contains("QR"))
                    {
                        MessageBox.Show($"Generating QR for {_pet.Name}...",
                            "QR Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };

                panelActions.Controls.Add(btn);
                btnX += 108;
            }

            //Shadows 

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(30, 59, 130, 246)))
                using (var path = RoundedPath(new Rectangle(35, 540, 98, 42), 12))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(30, 16, 185, 129)))
                using (var path = RoundedPath(new Rectangle(143, 540, 98, 42), 12))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(30, 220, 38, 38)))
                using (var path = RoundedPath(new Rectangle(251, 540, 98, 42), 12))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(30, 245, 158, 11)))
                using (var path = RoundedPath(new Rectangle(359, 540, 98, 42), 12))
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