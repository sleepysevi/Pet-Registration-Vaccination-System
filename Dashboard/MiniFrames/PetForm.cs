using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiniFrames
{
    public partial class PetForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private TextBox tbPetName;
        private ComboBox cbSpecies;
        private ComboBox cbBreed;
        private TextBox tbColor;
        private NumericUpDown nudAge;
        private ComboBox cbOwner;
        private PictureBox pbPhoto;
        private string selectedPhotoPath = string.Empty;

        private static readonly string[] DogBreeds = { "Aspin (Mixed)", "Golden Retriever", "Labrador", "Shih Tzu", "Pomeranian", "Siberian Husky", "German Shepherd", "Beagle", "Poodle", "Dachshund", "Other" };
        private static readonly string[] CatBreeds = { "Puspin (Mixed)", "Persian", "Siamese", "Maine Coon", "British Shorthair", "Ragdoll", "Scottish Fold", "Other" };
        private static readonly string[] BirdBreeds = { "Budgerigar", "Cockatiel", "Love Bird", "African Grey", "Macaw", "Canary", "Other" };
        private static readonly string[] OtherBreeds = { "Mixed", "Purebred", "Other" };
        public string PetNameValue { get; private set; } = "";
        public string SpeciesValue { get; private set; } = "";
        public string BreedValue { get; private set; } = "";
        public string ColorValue { get; private set; } = "";
        public int AgeValue { get; private set; }
        public string OwnerNameValue { get; private set; } = "";
        public string OwnerContactValue { get; private set; } = "";
        public string PhotoPathValue { get; private set; } = "";

        public PetForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "New Pet Registration";
            this.Size = new Size(500, 520);
            this.MinimumSize = new Size(500, 520);
            this.MaximumSize = new Size(500, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(237, 237, 237);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            // Scroll Panel 

            var scrollPanel = new Panel
            {
                Size = new Size(500, 520),
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
                Size = new Size(480, 80),
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
            scrollPanel.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "🐾",
                Font = new Font("Inter", 20, FontStyle.Regular),
                ForeColor = Color.FromArgb(134, 239, 172),
                Location = new Point(32, 26),
                Size = new Size(36, 36),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "New Pet Registration",
                Font = new Font("Poppins", 17, FontStyle.Bold),
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

            // Helpers

            Label MakeLabel(string text, int y) => new Label
            {
                Text = text,
                Font = new Font("Inter", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(28, y),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            (Panel w, TextBox tb) MakeTextBox(int y, int h, string ph, bool multi = false)
            {
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(440, h + 14),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, 20);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), 20))
                        g.DrawPath(pen, path);
                };
                var tb = new TextBox
                {
                    Size = new Size(wrapper.Width - 24, h),
                    Font = new Font("Inter", 12, FontStyle.Regular),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    BorderStyle = BorderStyle.None,
                    Multiline = multi,
                    WordWrap = multi,
                    ScrollBars = multi ? ScrollBars.Vertical : ScrollBars.None,
                    Text = ph
                };
                tb.Location = new Point(12, multi ? 8 : (wrapper.Height - tb.PreferredHeight) / 2);
                tb.Enter += (s, e) => { if (tb.Text == ph) { tb.Text = ""; tb.ForeColor = Color.FromArgb(30, 41, 59); } };
                tb.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = ph; tb.ForeColor = Color.FromArgb(100, 116, 139); } };
                wrapper.Controls.Add(tb);
                return (wrapper, tb);
            }

            (Panel w, ComboBox cb) MakeComboBox(int y, string[] items)
            {
                var wrapper = new Panel
                {
                    Location = new Point(28, y),
                    Size = new Size(440, 38),
                    BackColor = Color.FromArgb(241, 245, 249)
                };
                wrapper.Region = RoundedRegion(wrapper.Size, 20);
                wrapper.Paint += (s, e) =>
                {
                    var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                    using (var path = RoundedPath(new Rectangle(0, 0, wrapper.Width - 1, wrapper.Height - 1), 20))
                        g.DrawPath(pen, path);
                };
                var cb = new ComboBox
                {
                    Font = new Font("Inter", 12, FontStyle.Regular),
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

            //Fields 

            scrollPanel.Controls.Add(MakeLabel("Pet Name *", 96));
            var (wName, _tb1) = MakeTextBox(118, 24, "e.g. Buddy, Luna, Max");
            tbPetName = _tb1;
            scrollPanel.Controls.Add(wName);

            scrollPanel.Controls.Add(MakeLabel("Species *", 158));
            var (wSpecies, _cb1) = MakeComboBox(180, new[] { "Dog", "Cat", "Bird", "Other" });
            cbSpecies = _cb1;
            scrollPanel.Controls.Add(wSpecies);

            scrollPanel.Controls.Add(MakeLabel("Breed *", 220));
            var (wBreed, _cb2) = MakeComboBox(242, DogBreeds);
            cbBreed = _cb2;
            scrollPanel.Controls.Add(wBreed);

            cbSpecies.SelectedIndexChanged += (s, e) =>
            {
                cbBreed.Items.Clear();
                cbBreed.Items.AddRange(cbSpecies.Text switch
                {
                    "Dog" => DogBreeds,
                    "Cat" => CatBreeds,
                    "Bird" => BirdBreeds,
                    _ => OtherBreeds
                });
                cbBreed.SelectedIndex = 0;
            };

            scrollPanel.Controls.Add(MakeLabel("Color", 282));
            var (wColor, _tb2) = MakeTextBox(304, 24, "e.g. Brown, Black, White...");
            tbColor = _tb2;
            scrollPanel.Controls.Add(wColor);

            scrollPanel.Controls.Add(MakeLabel("Age (years)", 344));

            var wAge = new Panel
            {
                Location = new Point(28, 366),
                Size = new Size(160, 38),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            wAge.Region = RoundedRegion(wAge.Size, 20);
            wAge.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                using (var path = RoundedPath(new Rectangle(0, 0, wAge.Width - 1, wAge.Height - 1), 20))
                    g.DrawPath(pen, path);
            };
            nudAge = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 20,
                Value = 1,
                Font = new Font("Inter", 12, FontStyle.Regular),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(30, 41, 59),
                BorderStyle = BorderStyle.None,
                Size = new Size(wAge.Width - 24, 22)
            };
            nudAge.Location = new Point(12, (wAge.Height - nudAge.PreferredHeight) / 2);
            wAge.Controls.Add(nudAge);
            scrollPanel.Controls.Add(wAge);
            scrollPanel.Controls.Add(new Label
            {
                Text = "years old",
                Font = new Font("Inter", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(196, 374),
                AutoSize = true,
                BackColor = Color.Transparent
            });

            scrollPanel.Controls.Add(MakeLabel("Owner *", 414));
            var (wOwner, _cb3) = MakeComboBox(436, new[]
            {
                "— Select Owner —",
                "Jussa Largado (09171234567)",
                "Zaire Montealto (09281234567)",
                "+ Add New Owner..."
            });
            cbOwner = _cb3;
            scrollPanel.Controls.Add(wOwner);

            cbOwner.SelectedIndexChanged += (s, e) =>
            {
                if (cbOwner.Text != "+ Add New Owner...") return;
                cbOwner.SelectedIndex = 0;
                var ownerForm = new OwnerForm();
                ownerForm.FormClosed += (fs, fe) =>
                {
                    // TODO: reload owners from DB and repopulate cbOwner
                };
                ownerForm.ShowDialog(this);
            };

            // Photo 

            scrollPanel.Controls.Add(MakeLabel("Pet Photo", 492));

            var btnPhoto = new Button
            {
                Text = "Upload Photo",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(150, 44),
                Location = new Point(28, 514),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnPhoto.FlatAppearance.BorderSize = 0;
            btnPhoto.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnPhoto.Region = RoundedRegion(btnPhoto.Size, 20);
            scrollPanel.Controls.Add(btnPhoto);

            pbPhoto = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point(196, 510),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(226, 232, 240)
            };
            var circlePath = new GraphicsPath();
            circlePath.AddEllipse(0, 0, pbPhoto.Width, pbPhoto.Height);
            pbPhoto.Region = new Region(circlePath);
            pbPhoto.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (pbPhoto.Image == null)
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(203, 213, 225)), 0, 0, pbPhoto.Width, pbPhoto.Height);
                    g.DrawString("🐾", new Font("Segoe UI Emoji", 28),
                        new SolidBrush(Color.FromArgb(148, 163, 184)),
                        new RectangleF(0, 0, pbPhoto.Width, pbPhoto.Height),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                using (var pen = new Pen(Color.FromArgb(148, 163, 184), 2f))
                    g.DrawEllipse(pen, 1, 1, pbPhoto.Width - 3, pbPhoto.Height - 3);
            };
            scrollPanel.Controls.Add(pbPhoto);

            btnPhoto.Click += (s, e) =>
            {
                using (var dlg = new OpenFileDialog
                {
                    Title = "Select Pet Photo",
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
                })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        pbPhoto.Image = Image.FromFile(dlg.FileName);
                        selectedPhotoPath = dlg.FileName;
                        pbPhoto.Invalidate();
                    }
                }
            };

            scrollPanel.Controls.Add(new Label
            {
                Text = "📱 QR Code: PET-[ID] will be generated automatically",
                Font = new Font("Inter", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(28, 622),
                Size = new Size(440, 22),
                BackColor = Color.Transparent
            });

            // Buttons 

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(120, 42),
                Location = new Point(62, 658),
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

            var btnSave = new Button
            {
                Text = "Register Pet",
                Font = new Font("Inter", 12, FontStyle.Bold),
                Size = new Size(185, 42),
                Location = new Point(230, 658),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btnSave.Region = RoundedRegion(btnSave.Size, 20);
            btnSave.Click += OnRegisterPet;
            scrollPanel.Controls.Add(btnSave);

            // Button Shadows

            scrollPanel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(64, 660, 120, 42), 20))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(50, 4, 120, 87)))
                using (var path = RoundedPath(new Rectangle(232, 660, 185, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        private void OnRegisterPet(object? sender, EventArgs e)
        {
            if (tbPetName == null || string.IsNullOrWhiteSpace(tbPetName.Text) || tbPetName.Text == "e.g. Buddy, Luna, Max")
            {
                MessageBox.Show("Please enter the pet's name.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbOwner == null || cbOwner.SelectedIndex <= 0 || cbOwner.Text.StartsWith("—"))
            {
                MessageBox.Show("Please select an owner.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string petName = tbPetName.Text.Trim();
            string ownerText = cbOwner.Text; 

            // Split name and contact from the owner text
            string ownerName = ownerText.Contains("(")
                ? ownerText.Substring(0, ownerText.IndexOf("(")).Trim()
                : ownerText;

            string contact = ownerText.Contains("(")
                ? ownerText.Substring(ownerText.IndexOf("(") + 1).TrimEnd(')')
                : ownerText;

            PetNameValue = petName;
            SpeciesValue = cbSpecies.Text.Trim();
            BreedValue = cbBreed.Text.Trim();
            ColorValue = tbColor.Text == "e.g. Brown, Black, White..." ? string.Empty : tbColor.Text.Trim();
            AgeValue = (int)nudAge.Value;
            OwnerNameValue = ownerName;
            OwnerContactValue = contact;
            PhotoPathValue = selectedPhotoPath;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

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