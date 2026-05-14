using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MiniFrames;
using AlagaTrack;

namespace AlagaTrackFrontEnd
{
    // Owners management page.
    public partial class OwnersForm : Form
    {
        private readonly OwnerManager _ownerManager = new OwnerManager();
        private List<Owner> _owners = new List<Owner>();

        public OwnersForm()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnRefresh.Click += (_, _) => LoadOwners();
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            grid.CellDoubleClick += Grid_CellDoubleClick;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnShown(System.EventArgs e)
        {
            base.OnShown(e);
            if (UiRoundHelper.IsInDesignMode(this)) return;
            UiRoundHelper.ApplyRoundedTheme(this);
            UiRoundHelper.ConfigureTableBehavior(this);

            // Refresh cards so custom shadow paint is visible after layout is finalized.
            card1.Invalidate();
            card2.Invalidate();
            card3.Invalidate();
        }

        private void card3_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card1_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card2_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void OwnersForm_Load(object sender, System.EventArgs e)
        {
            LoadOwners();
        }

        private void tablePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            using (var dialog = new OwnerForm())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreatedOwner != null)
                {
                    var owner = dialog.CreatedOwner;
                    if (_ownerManager.AddOwner(owner.Name, owner.Address, owner.ContactNumber, owner.Email, owner.Notes))
                    {
                        LoadOwners();
                    }
                }
            }
        }

        private void Grid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _owners.Count)
            {
                return;
            }

            var selectedOwner = _owners[e.RowIndex];
            using (var dialog = new EditOwnerForm(selectedOwner))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK && dialog.UpdatedOwner != null)
                {
                    var updated = dialog.UpdatedOwner;
                    var record = new OwnerRecord
                    {
                        OwnerID = updated.OwnerID,
                        Name = updated.Name,
                        ContactNumber = updated.ContactNumber,
                        Address = updated.Address,
                        Email = updated.Email,
                        Notes = updated.Notes
                    };

                    if (_ownerManager.UpdateOwner(record))
                    {
                        LoadOwners();
                    }
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (grid.CurrentRow == null)
            {
                MessageBox.Show("Select an owner to delete.", "Delete Owner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var owner = _owners[grid.CurrentRow.Index];
            var confirm = MessageBox.Show(
                $"Delete owner '{owner.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            if (_ownerManager.DeleteOwner(owner.OwnerID))
            {
                LoadOwners();
            }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var search = txtSearch.Text?.Trim() ?? string.Empty;
            if (search.Equals("Search owners...", StringComparison.OrdinalIgnoreCase))
            {
                search = string.Empty;
            }

            LoadOwners(search);
        }

        private void LoadOwners(string? search = null)
        {
            var records = _ownerManager.GetOwners(search);
            _owners = records.Select(r => new Owner
            {
                OwnerID = r.OwnerID,
                Name = r.Name,
                ContactNumber = r.ContactNumber,
                Address = r.Address,
                Email = r.Email,
                Notes = r.Notes
            }).ToList();
            grid.Rows.Clear();

            foreach (var owner in _owners)
            {
                grid.Rows.Add(
                    owner.OwnerID,
                    owner.Name,
                    owner.ContactNumber,
                    owner.Address,
                    "-", // pets count placeholder until pets grid is wired
                    "Double-click to edit");
            }

            var stats = _ownerManager.GetOwnerStats();
            lblCard1Value.Text = stats.TotalOwners.ToString();
            lblCard2Value.Text = stats.NewToday.ToString();
            lblCard3Value.Text = stats.AvgPetsPerOwner.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }
}

