using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MiniFrames
{
    public partial class ViewOwnerPetsForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private readonly Owner _owner;
        private readonly List<Pet> _pets;

        public ViewOwnerPetsForm(Owner owner, List<Pet> pets)
        {
            _owner = owner;
            _pets = pets;
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = $"{_owner.Name}'s Pets";
            this.Size = new Size(850, 550);
            this.MinimumSize = new Size(850, 550);
            this.MaximumSize = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(237, 237, 237);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Region = RoundedRegion(this.Size, 20);

            //  Header 

            var panelHeader = new Panel
            {
                Size = new Size(830, 80),
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
            this.Controls.Add(panelHeader);

            var lblIcon = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI Emoji", 20),
                ForeColor = Color.FromArgb(147, 197, 253),
                Location = new Point(32, 22),
                Size = new Size(40, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = $"{_owner.Name} ({_owner.ContactNumber}) - Pets",
                Font = new Font("Inter", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(80, 28),
                Size = new Size(620, 36),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panelHeader.Controls.Add(lblTitle);

            var btnClose = new Button
            {
                Text = "X",
                Font = new Font("Inter", 13, FontStyle.Bold),
                Size = new Size(38, 38),
                Location = new Point(762, 21),
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

            // Owner Info Card 

            var cardOwner = new Panel
            {
                Size = new Size(790, 56),
                Location = new Point(30, 94),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardOwner.Region = RoundedRegion(cardOwner.Size, 16);
            cardOwner.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.FromArgb(125, 211, 252), 1.5f))
                using (var path = RoundedPath(new Rectangle(0, 0, cardOwner.Width - 1, cardOwner.Height - 1), 16))
                    g.DrawPath(pen, path);
            };
            this.Controls.Add(cardOwner);

            var lblOwnerInfo = new Label
            {
                Text = $"📞  {_owner.ContactNumber}     🏠  {_owner.Address}     📝  {_owner.Notes}",
                Font = new Font("Inter", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(20, 0),
                Size = new Size(750, 56),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            cardOwner.Controls.Add(lblOwnerInfo);

            // Table Title

            var lblTableTitle = new Label
            {
                Text = $"{_owner.Name}'s Pets",
                Font = new Font("Inter", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(30, 162),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblTableTitle);

            // Pets DataGridView

            var dgv = new DataGridView
            {
                Location = new Point(30, 188),
                Size = new Size(790, 248),
                BackgroundColor = Color.FromArgb(241, 245, 249),
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(226, 232, 240),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Inter", 11),
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ScrollBars = ScrollBars.Vertical,
                Cursor = Cursors.Hand
            };

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.Padding = new Padding(8, 10, 8, 10);
            dgv.DefaultCellStyle.Font = new Font("Inter", 11);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Inter", 11, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.EnableHeadersVisualStyles = false;

            // Columns 

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhoto",
                HeaderText = "Photo",
                Width = 80,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Name",
                Width = 130,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBreed",
                HeaderText = "Breed",
                Width = 155,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAge",
                HeaderText = "Age",
                Width = 60,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLastVaccine",
                HeaderText = "Last Vaccine",
                Width = 140,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                Width = 90,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colAction",
                HeaderText = "Action",
                Text = "Edit Pet",
                UseColumnTextForButtonValue = true,
                Width = 115,
                FlatStyle = FlatStyle.Flat
            });

            // Rows

            foreach (var pet in _pets)
            {
                string photo = pet.Species switch
                {
                    "Cat" => "🐈",
                    "Bird" => "🐦",
                    _ => "🐕"
                };
                string lastVaccine = string.IsNullOrEmpty(pet.LastVaccineDate)
                    ? "Never" : pet.LastVaccineDate;
                string status = string.IsNullOrEmpty(pet.LastVaccineDate)
                    ? "🔴 Never"
                    : pet.VaccineStatus == "Due Soon" ? "🟡 Due Soon" : "🟢 OK";

                dgv.Rows.Add(photo, pet.Name, pet.Breed,
                             $"{pet.Age} yrs", lastVaccine, status);
            }

            // Cell Formatting 

            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == dgv.Columns["colPhoto"].Index)
                {
                    e.CellStyle.Font = new Font("Segoe UI Emoji", 18);
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (e.ColumnIndex == dgv.Columns["colLastVaccine"].Index)
                {
                    e.CellStyle.ForeColor = e.Value?.ToString() == "Never"
                        ? Color.FromArgb(239, 68, 68)
                        : Color.FromArgb(16, 185, 129);
                    e.CellStyle.Font = new Font("Inter", 11, FontStyle.Bold);
                }
                if (e.ColumnIndex == dgv.Columns["colStatus"].Index)
                {
                    var val = e.Value?.ToString() ?? "";
                    e.CellStyle.ForeColor = val.Contains("Never") ? Color.FromArgb(239, 68, 68)
                        : val.Contains("Due") ? Color.FromArgb(245, 158, 11)
                        : Color.FromArgb(16, 185, 129);
                    e.CellStyle.Font = new Font("Inter", 11, FontStyle.Bold);
                }
            };

            // Edit Button Paint 

            dgv.CellPainting += (s, e) =>
            {
                if (e.ColumnIndex == dgv.Columns["colAction"].Index && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Background |
                                          DataGridViewPaintParts.Border);
                    var btnRect = new Rectangle(
                        e.CellBounds.X + 10,
                        e.CellBounds.Y + 9,
                        e.CellBounds.Width - 20,
                        e.CellBounds.Height - 18);
                    using (var path = RoundedPath(btnRect, 10))
                    using (var brush = new SolidBrush(Color.FromArgb(59, 130, 246)))
                        e.Graphics.FillPath(brush, path);
                    TextRenderer.DrawText(e.Graphics, "Edit Pet",
                        new Font("Inter", 10, FontStyle.Bold),
                        btnRect, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            };

            dgv.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == dgv.Columns["colAction"].Index && e.RowIndex >= 0)
                {
                    var pet = _pets[e.RowIndex];
                    var editForm = new EditPetForm(pet);
                    editForm.ShowDialog(this);
                }
            };

            this.Controls.Add(dgv);

            // Actions Row 

            var panelActions = new Panel
            {
                Size = new Size(790, 56),
                Location = new Point(30, 450),
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

            var btnAddPet = new Button
            {
                Text = $"+ Add Pet for {_owner.Name}",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(220, 38),
                Location = new Point(12, 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnAddPet.FlatAppearance.BorderSize = 0;
            btnAddPet.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btnAddPet.Region = RoundedRegion(btnAddPet.Size, 12);
            btnAddPet.Click += (s, e) =>
            {
                // TODO: open AddPetForm pre-filled with owner
                MessageBox.Show($"Opening Add Pet for {_owner.Name}...",
                    "Add Pet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            panelActions.Controls.Add(btnAddPet);

            var btnPrint = new Button
            {
                Text = "Print Owner Report",
                Font = new Font("Inter", 11, FontStyle.Bold),
                Size = new Size(220, 38),
                Location = new Point(558, 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnPrint.Region = RoundedRegion(btnPrint.Size, 12);
            btnPrint.Click += (s, e) =>
            {
                // TODO: plug in print/report logic
                MessageBox.Show($"Printing report for {_owner.Name}...",
                    "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            panelActions.Controls.Add(btnPrint);

            //Shadows 

            this.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(40, 16, 185, 129)))
                using (var path = RoundedPath(new Rectangle(32, 452, 220, 38), 12))
                    g.FillPath(brush, path);
                using (var brush = new SolidBrush(Color.FromArgb(40, 59, 130, 246)))
                using (var path = RoundedPath(new Rectangle(590, 452, 220, 38), 12))
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