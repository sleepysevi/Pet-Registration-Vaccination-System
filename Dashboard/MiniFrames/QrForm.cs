using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using QRCoder;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class QrForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private readonly string _petId;
        private readonly string _petName;
        private readonly string _contactNumber;
        private readonly string _ownerName;

        private Panel? _qrStickerPanel;

        public QrForm(string petId, string petName, string contactNumber, string ownerName)
        {
            _petId = petId;
            _petName = petName;
            _contactNumber = contactNumber;
            _ownerName = ownerName;

            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "QR Code";
            this.Size = new Size(420, 520);
            this.MinimumSize = new Size(420, 520);
            this.MaximumSize = new Size(420, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 16);

            //  Close Button 

            var btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Inter", 16, FontStyle.Bold),
                Size = new Size(32, 32),
                Location = new Point(376, 14),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);
            btnClose.MouseEnter += (s, e) => btnClose.ForeColor = Color.White;
            btnClose.MouseLeave += (s, e) => btnClose.ForeColor = Color.FromArgb(148, 163, 184);
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            this.MouseDown += DragForm;

            //  Main Title 

            var lblTitle = new Label
            {
                Text = $"{_petId} Registered!",
                Font = new Font("Inter", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(112, 78, 45),
                Location = new Point(30, 25),
                Size = new Size(360, 40),
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblTitle);

            //Pet Info

            var lblPetName = new Label
            {
                Text = $"Pet: {_petName}",
                Font = new Font("Inter", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(30, 72),
                Size = new Size(180, 28), 
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblPetName);

            var lblContact = new Label
            {
                Text = $"📞 {_contactNumber}",
                Font = new Font("Inter", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(30, 100),
                Size = new Size(360, 26),
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblContact);

            var lblOwner = new Label
            {
                Text = $"👤 {_ownerName}",
                Font = new Font("Inter", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(210, 72), 
                Size = new Size(180, 28),
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblOwner);

            // QR Sticker Preview Panel 

            _qrStickerPanel = new Panel
            {
                Size = new Size(320, 300),
                Location = new Point(50, 140),
                BackColor = Color.White
            };
            _qrStickerPanel.Region = RoundedRegion(_qrStickerPanel.Size, 16);
            _qrStickerPanel.Paint += PaintQrSticker;
            this.Controls.Add(_qrStickerPanel);

            // Print Button 

            var btnPrint = new Button
            {
                Text = "Print QR",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(162, 50),
                Location = new Point(30, 458),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btnPrint.Region = RoundedRegion(btnPrint.Size, 14);
            btnPrint.Click += OnPrint;
            this.Controls.Add(btnPrint);

            //  Save PNG Button 

            var btnSave = new Button
            {
                Text = "Save PNG",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(162, 50),
                Location = new Point(224, 458),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnSave.Region = RoundedRegion(btnSave.Size, 14);
            btnSave.Click += OnSavePng;
            this.Controls.Add(btnSave);

            // Form-level shadows 

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Sticker shadow
                DrawShadow(g,
                    new Rectangle(50, 138, 320, 300),
                    16, Color.FromArgb(25, 0, 0, 0), 8);

                // Print button shadow
                DrawShadow(g,
                    new Rectangle(30, 458, 162, 50),
                    14, Color.FromArgb(60, 16, 185, 129), 6);

                // Save button shadow
                DrawShadow(g,
                    new Rectangle(224, 458, 162, 50),
                    14, Color.FromArgb(60, 59, 130, 246), 6);
            };
        }

        // QR Sticker Painter 

        private void PaintQrSticker(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel panel) return;
            var g = e.Graphics;
            int w = panel.Width;
            int h = panel.Height;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // White background
            g.Clear(Color.White);

            // Top accent bar
            using (var brush = new LinearGradientBrush(
                       new Point(w, 0), new Point(0,36),
                       Color.White,
                       Color.FromArgb(255, 221, 87)))
            using (var path = RoundedPathTopOnly(new Rectangle(0, 0, w, 36), 16))
                g.FillPath(brush, path);

            // PET-ID on accent bar
            using (var font = new Font("Consolas", 14, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
                g.DrawString(_petId, font, brush, new PointF(14, 8));

            // QR Code
            var qrRect = new Rectangle((w - 200) / 2, 50, 200, 200);
            var qrBitmap = GenerateQrBitmap($"Pet ID: {_petId}\nName: {_petName}\nOwner: {_ownerName}\nContact: {_contactNumber}", qrRect.Width);
            g.DrawImage(qrBitmap, qrRect);
            qrBitmap.Dispose();

            // Border around QR
            using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                g.DrawRectangle(pen, qrRect);

            // Pet name below QR
            using (var font = new Font("Inter", 13, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(32, 47, 124)))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString(_petName, font, brush,
                    new RectangleF(0, 258, w, 22), sf);
            }

            // Footer brand
            using (var font = new Font("Inter", 9, FontStyle.Regular))
            using (var brush = new SolidBrush(Color.FromArgb(148, 163, 184)))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("Powered by PawTech", font, brush,
                    new RectangleF(0, 278, w, 18), sf);
            }

            // Outer border
            using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
            using (var path = RoundedPath(new Rectangle(0, 0, w - 1, h - 1), 16))
                g.DrawPath(pen, path);
        }

        private Bitmap GenerateQrBitmap(string content, int size)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrData);
            return qrCode.GetGraphic(size / 21); 
        }

        // Print

        private void OnPrint(object? sender, EventArgs e)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += (ps, pe) =>
            {
                var bmp = RenderStickerBitmap();
                pe.Graphics.DrawImage(bmp, pe.MarginBounds);
                bmp.Dispose();
            };

            using (var dlg = new PrintDialog { Document = pd })
                if (dlg.ShowDialog() == DialogResult.OK)
                    pd.Print();
        }

        // Save PNG 

        private void OnSavePng(object? sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog
            {
                Title = "Save QR Sticker",
                FileName = $"{_petId}_QR",
                Filter = "PNG Image|*.png",
                DefaultExt = "png"
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var bmp = RenderStickerBitmap();
                    bmp.Save(dlg.FileName, ImageFormat.Png);
                    bmp.Dispose();
                    MessageBox.Show("Sticker saved successfully!", "Saved",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Renders the sticker panel to a Bitmap for print / save
        private Bitmap RenderStickerBitmap()
        {
            if (_qrStickerPanel == null) return new Bitmap(1, 1);
            var bmp = new Bitmap(_qrStickerPanel.Width, _qrStickerPanel.Height);
            _qrStickerPanel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
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