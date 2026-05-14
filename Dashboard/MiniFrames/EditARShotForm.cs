using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniFrames
{
    public partial class EditARShotForm : Form
    {
        private readonly Vaccination _editingVaccination;

        private ComboBox cbOwner;
        private ComboBox cbPet;
        private TextBox tbDateGiven;
        private TextBox tbNextDue;
        private TextBox tbNotes;

        public EditARShotForm(Vaccination editingVaccination)
        {
            _editingVaccination = editingVaccination;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Form 
            this.Text = "Edit AR Shot";
            this.Size = new Size(490, 520);
            this.MinimumSize = new Size(490, 520);
            this.MaximumSize = new Size(490, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            //  Sample Data 
            var ownerPets = new Dictionary<string, List<string>>
            {
                { "John Doe  +639123456789",  new List<string> { "Buddy", "Max", "Whiskers" } },
                { "Jane Smith  +639987654321", new List<string> { "Bella", "Luna" } },
                { "Carlos Reyes  +639111222333", new List<string> { "Rocky" } }
            };

            // Header Panel 
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
                    Color.FromArgb(245, 158, 11),   
                    Color.FromArgb(217, 119, 6)))  
                {
                    g.FillPath(brush, path);
                }
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "💉",
                Font = new Font("Inter", 22, FontStyle.Regular),
                ForeColor = Color.FromArgb(254, 243, 199),
                Location = new Point(32, 28),
                Size = new Size(38, 38),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"Edit AR Shot: {_editingVaccination.PetName}",
                Font = new Font("Inter", 18, FontStyle.Bold),
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
            btnClose.Click += (s, e) => this.Close();
            panelHeader.Controls.Add(btnClose);

            //  Field Helpers 
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
                    ForeColor = Color.FromArgb(30, 41, 59),
                    FlatStyle = FlatStyle.Flat,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cb.Items.Add(hint);
                cb.SelectedIndex = 0;
                wrapper.Controls.Add(cb);
                return (wrapper, cb);
            }

            (Panel wrapper, TextBox tb) MakeTextBox(int y, int height, string value, bool multiline = false, float fontSize = 12f)
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
                tb.Location = new Point(12, multiline ? 8 : (wrapper.Height - tb.PreferredHeight) / 2);
                wrapper.Controls.Add(tb);
                return (wrapper, tb);
            }

            // Owner dropdown (pre-filled)
            this.Controls.Add(MakeLabel("Owner", 100));
            var (ownerWrapper, _cbOwner) = MakeComboBox(122, "— Select Owner —");
            cbOwner = _cbOwner;
            foreach (var owner in ownerPets.Keys)
                cbOwner.Items.Add(owner);

            // Pre-select matching owner
            int ownerIdx = cbOwner.Items.IndexOf(_editingVaccination.OwnerName);
            cbOwner.SelectedIndex = ownerIdx >= 0 ? ownerIdx : 0;
            this.Controls.Add(ownerWrapper);

            // Pet dropdown (pre-filled) 
            this.Controls.Add(MakeLabel("Pet", 172));
            var (petWrapper, _cbPet) = MakeComboBox(194, "— Select Owner First —");
            cbPet = _cbPet;

            // Pre-populate pet list based on existing owner
            if (ownerIdx >= 0 && ownerPets.ContainsKey(_editingVaccination.OwnerName))
            {
                cbPet.Items.Clear();
                cbPet.Items.Add("— Select Pet —");
                foreach (var pet in ownerPets[_editingVaccination.OwnerName])
                    cbPet.Items.Add(pet);
                cbPet.Enabled = true;
                cbPet.ForeColor = Color.FromArgb(30, 41, 59);

                int petIdx = cbPet.Items.IndexOf(_editingVaccination.PetName);
                cbPet.SelectedIndex = petIdx >= 0 ? petIdx : 0;
            }
            else
            {
                cbPet.Enabled = false;
                cbPet.ForeColor = Color.FromArgb(100, 116, 139);
            }
            this.Controls.Add(petWrapper);

            // Wire Owner → Pet
            cbOwner.SelectedIndexChanged += (s, e) =>
            {
                cbPet.Items.Clear();
                string selectedOwner = cbOwner.SelectedItem?.ToString() ?? "";
                if (cbOwner.SelectedIndex == 0 || !ownerPets.ContainsKey(selectedOwner))
                {
                    cbPet.Items.Add("— Select Owner First —");
                    cbPet.SelectedIndex = 0;
                    cbPet.Enabled = false;
                    cbPet.ForeColor = Color.FromArgb(100, 116, 139);
                }
                else
                {
                    cbPet.Enabled = true;
                    cbPet.ForeColor = Color.FromArgb(30, 41, 59);
                    cbPet.Items.Add("— Select Pet —");
                    foreach (var pet in ownerPets[selectedOwner])
                        cbPet.Items.Add(pet);
                    cbPet.SelectedIndex = 0;
                }
            };

            // Remaining fields (pre-filled) 
            this.Controls.Add(MakeLabel("Date Given", 244));
            var (wDate, _tbDate) = MakeTextBox(266, 24, _editingVaccination.DateGiven, fontSize: 12f);
            tbDateGiven = _tbDate;
            this.Controls.Add(wDate);

            this.Controls.Add(MakeLabel("Next Due", 306));
            var (wNext, _tbNext) = MakeTextBox(328, 24, _editingVaccination.NextDueDate, fontSize: 12f);
            tbNextDue = _tbNext;
            this.Controls.Add(wNext);

            this.Controls.Add(MakeLabel("Notes", 368));
            var (wNotes, _tbNotes) = MakeTextBox(390, 54, _editingVaccination.Notes, multiline: true, fontSize: 12f);
            tbNotes = _tbNotes;
            this.Controls.Add(wNotes);

            // Buttons 
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
            btnCancel.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Text = "💉 Update Shot",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(175, 42),
                Location = new Point(222, 462),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 158, 11),  
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 119, 6);
            btnSave.Region = RoundedRegion(btnSave.Size, 20);
            btnSave.Click += OnUpdateShot;
            this.Controls.Add(btnSave);

            // Button shadows
            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(104, 464, 110, 42), 20))
                    g.FillPath(brush, path);

                using (var brush = new SolidBrush(Color.FromArgb(50, 245, 158, 11)))
                using (var path = RoundedPath(new Rectangle(224, 464, 175, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        // Update Handler 
        private void OnUpdateShot(object? sender, EventArgs e)
        {
            if (cbOwner.SelectedIndex <= 0 || cbOwner.Text.StartsWith("—"))
            {
                MessageBox.Show("Please select an owner.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!cbPet.Enabled || cbPet.SelectedIndex <= 0 || cbPet.Text.StartsWith("—"))
            {
                MessageBox.Show("Please select a pet.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDateGiven.Text))
            {
                MessageBox.Show("Please enter the date given.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedVaccination = new Vaccination
            {
                VaccinationID = _editingVaccination.VaccinationID,
                OwnerName = cbOwner.Text,
                PetName = cbPet.Text,
                DateGiven = tbDateGiven.Text.Trim(),
                NextDueDate = tbNextDue.Text.Trim(),
                Notes = tbNotes.Text.Trim()
            };

            // TODO: plug in controller when backend is ready
            // await controller.UpdateVaccination(updatedVaccination.VaccinationID, updatedVaccination);

            MessageBox.Show(
                $"AR Shot updated!\n\nPet: {updatedVaccination.PetName}\nDate Given: {updatedVaccination.DateGiven}\nNext Due: {updatedVaccination.NextDueDate}",
                "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        //  Rounded Helpers 
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