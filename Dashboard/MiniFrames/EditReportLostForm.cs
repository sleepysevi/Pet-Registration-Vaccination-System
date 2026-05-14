using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniFrames
{
    public partial class EditReportLostForm : Form
    {
        private readonly LostPetReport _editingReport;

        private ComboBox cbOwnerSearch;
        private ComboBox cbPet;
        private DateTimePicker dtpLostDate;
        private TextBox tbLocation;
        private TextBox tbDescription;

        public EditReportLostForm(LostPetReport editingReport)
        {
            _editingReport = editingReport;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Form 
            this.Text = "Edit Lost Report";
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
                { "John Doe  +639123456789",     new List<string> { "Buddy (Golden Retriever)", "Max (Aspin)", "Whiskers (Persian)" } },
                { "Jane Smith  +639987654321",   new List<string> { "Bella (Shih Tzu)", "Luna (Siamese)" } },
                { "Carlos Reyes  +639111222333", new List<string> { "Rocky (Labrador)" } }
            };
            var allOwners = new List<string>(ownerPets.Keys);

            // Header 
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
                    Color.FromArgb(245, 158, 11),   
                    Color.FromArgb(217, 119, 6)))    
                    g.FillPath(brush, path);
            };
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "🚨",
                Font = new Font("Inter", 18, FontStyle.Regular),
                ForeColor = Color.FromArgb(254, 243, 199),
                Location = new Point(32, 23),
                Size = new Size(34, 34),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"Edit Lost Report: {_editingReport.PetName}",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(76, 25),
                Size = new Size(330, 30),
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

            //  Field Helpers 
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

            (Panel w, TextBox tb) MakeTextBox(int y, int height, string value,
                              bool multiline = false, float fontSize = 12f, bool readOnly = false)
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
                    ForeColor = Color.FromArgb(30, 41, 59),
                    BorderStyle = BorderStyle.None,
                    Multiline = multiline,
                    WordWrap = multiline,
                    ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None,
                    ReadOnly = readOnly,
                    Text = value
                };
                tb.Location = new Point(12, multiline ? 8 : (w.Height - tb.PreferredHeight) / 2);
                w.Controls.Add(tb);
                return (w, tb);
            }

            (Panel w, DateTimePicker dtp) MakeDatePicker(int y, DateTime value)
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
                    Value = value
                };
                w.Controls.Add(dtp);
                return (w, dtp);
            }

            //  Owner searchable field (pre-filled) 
            scroll.Controls.Add(MakeLabel("Owner", 10));

            var ownerWrapper = new Panel
            {
                Location = new Point(24, 32),
                Size = new Size(440, 38),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            ownerWrapper.Region = RoundedRegion(ownerWrapper.Size, 20);
            ownerWrapper.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.8f))
                using (var path = RoundedPath(new Rectangle(0, 0, ownerWrapper.Width - 1, ownerWrapper.Height - 1), 20))
                    g.DrawPath(pen, path);
            };

            var _cbOwnerSearch = new ComboBox
            {
                Location = new Point(10, 6),
                Size = new Size(ownerWrapper.Width - 20, 26),
                Font = new Font("Inter", 12, FontStyle.Regular),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.None
            };
            cbOwnerSearch = _cbOwnerSearch;
            foreach (var o in allOwners) cbOwnerSearch.Items.Add(o);
            cbOwnerSearch.Text = _editingReport.OwnerName;
            ownerWrapper.Controls.Add(cbOwnerSearch);
            scroll.Controls.Add(ownerWrapper);

            // Pet dropdown (pre-filled) 
            scroll.Controls.Add(MakeLabel("Pet", 82));
            var (petW, _cbPet) = MakeComboBox(104, "— Select Owner First —");
            cbPet = _cbPet;

            // Pre-populate pet list based on existing owner
            if (ownerPets.ContainsKey(_editingReport.OwnerName))
            {
                cbPet.Items.Clear();
                cbPet.Items.Add("— Select Pet —");
                foreach (var pet in ownerPets[_editingReport.OwnerName])
                    cbPet.Items.Add(pet);
                cbPet.Enabled = true;
                cbPet.ForeColor = Color.FromArgb(30, 41, 59);

                int petIdx = cbPet.Items.IndexOf(_editingReport.PetName);
                cbPet.SelectedIndex = petIdx >= 0 ? petIdx : 0;
            }
            else
            {
                cbPet.Enabled = false;
                cbPet.ForeColor = Color.FromArgb(100, 116, 139);
            }
            scroll.Controls.Add(petW);

            //  Owner filter + pet wire logic
            bool isFiltering = false;
            cbOwnerSearch.TextChanged += (s, e) =>
            {
                if (isFiltering) return;
                isFiltering = true;

                string query = cbOwnerSearch.Text;
                int cursorPos = query.Length;

                cbOwnerSearch.Items.Clear();
                foreach (var o in allOwners)
                    if (o.ToLower().Contains(query.ToLower()))
                        cbOwnerSearch.Items.Add(o);

                cbOwnerSearch.Text = query;
                cbOwnerSearch.SelectionStart = cursorPos;
                cbOwnerSearch.SelectionLength = 0;

                if (cbOwnerSearch.Items.Count > 0)
                    cbOwnerSearch.DroppedDown = true;

                isFiltering = false;
            };

            cbOwnerSearch.SelectedIndexChanged += (s, e) =>
            {
                if (cbOwnerSearch.SelectedItem == null) return;
                string selected = cbOwnerSearch.SelectedItem.ToString();
                cbOwnerSearch.ForeColor = Color.FromArgb(30, 41, 59);

                cbPet.Items.Clear();
                if (ownerPets.ContainsKey(selected))
                {
                    cbPet.Enabled = true;
                    cbPet.ForeColor = Color.FromArgb(30, 41, 59);
                    cbPet.Items.Add("— Select Pet —");
                    foreach (var pet in ownerPets[selected])
                        cbPet.Items.Add(pet);
                    cbPet.SelectedIndex = 0;
                }
            };

            //Remaining fields (pre-filled) 
            scroll.Controls.Add(MakeLabel("Lost Date", 154));
            var (wDate, _dtp) = MakeDatePicker(176, _editingReport.DateLost);
            dtpLostDate = _dtp;
            scroll.Controls.Add(wDate);

            scroll.Controls.Add(MakeLabel("Last Seen Location", 226));
            var (wLoc, _tbLoc) = MakeTextBox(248, 24, _editingReport.LastSeenLocation);
            tbLocation = _tbLoc;
            scroll.Controls.Add(wLoc);

            scroll.Controls.Add(MakeLabel("Pet Description", 300));
            var (wDesc, _tbDesc) = MakeTextBox(322, 100, _editingReport.Description,
                multiline: true, fontSize: 11f);
            tbDescription = _tbDesc;
            scroll.Controls.Add(wDesc);

            // Buttons panel (fixed at bottom) 
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
                Text = "🚨 Update Report",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(195, 42),
                Location = new Point(220, 8),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 158, 11),   // #F59E0B orange
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnReport.FlatAppearance.BorderSize = 0;
            btnReport.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 119, 6);
            btnReport.Region = RoundedRegion(btnReport.Size, 20);
            btnReport.Click += OnUpdateReport;
            panelButtons.Controls.Add(btnReport);

            // Button shadows
            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                using (var path = RoundedPath(new Rectangle(76, 476, 130, 42), 20))
                    g.FillPath(brush, path);

                using (var brush = new SolidBrush(Color.FromArgb(50, 245, 158, 11)))
                using (var path = RoundedPath(new Rectangle(222, 476, 195, 42), 20))
                    g.FillPath(brush, path);
            };
        }

        // Update Handler 
        private void OnUpdateReport(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbOwnerSearch.Text))
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
            if (string.IsNullOrWhiteSpace(tbLocation.Text))
            {
                MessageBox.Show("Please enter the last seen location.", "Missing Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedReport = new LostPetReport
            {
                ReportID = _editingReport.ReportID,
                OwnerName = cbOwnerSearch.Text.Trim(),
                PetName = cbPet.Text,
                DateLost = dtpLostDate.Value,
                LastSeenLocation = tbLocation.Text.Trim(),
                Description = tbDescription.Text.Trim()
            };

            // TODO: plug in controller when backend is ready
            // await controller.UpdateLostReport(updatedReport.ReportID, updatedReport);

            MessageBox.Show(
                $"Lost report updated!\n\nPet: {updatedReport.PetName}\nLast Seen: {updatedReport.LastSeenLocation}\nDate: {updatedReport.DateLost:d}",
                "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
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