using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AlagaTrack;
using MiniFrames;

namespace AlagaTrackFrontEnd
{
    public partial class VaccinationsForm : Form
    {
        private readonly VaccinationManager _vacMgr = new VaccinationManager();
        private List<VaccinationRecord> _rows = new List<VaccinationRecord>();

        public VaccinationsForm()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnRefresh.Click += (_, _) => ReloadAll();
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (UiRoundHelper.IsInDesignMode(this)) return;
            UiRoundHelper.ApplyRoundedTheme(this);
            UiRoundHelper.ConfigureTableBehavior(this);

            card1.Invalidate();
            card2.Invalidate();
            card3.Invalidate();
            card4.Invalidate();
            ReloadAll();
        }

        private void card1_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card2_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card3_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card4_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search pets...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void ReloadAll()
        {
            lblCard1Value.Text = _vacMgr.GetTotalShots().ToString("00");
            lblCard2Value.Text = _vacMgr.GetDueSoonCount(30).ToString("00");
            lblCard3Value.Text = _vacMgr.GetOverdueCount().ToString("00");
            var (vac, tot) = _vacMgr.GetVaccinationCoverage();
            int pct = tot <= 0 ? 0 : (int)Math.Round(100.0 * vac / tot);
            lblCard4Value.Text = $"{pct}%";

            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search pets...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void LoadGrid(string? search)
        {
            _rows = _vacMgr.GetVaccinations(string.IsNullOrWhiteSpace(search) ? null : search);
            grid.Rows.Clear();

            var today = DateTime.Today;
            foreach (var v in _rows)
            {
                string status = "OK";
                if (DateTime.TryParse(v.NextDueDate, out var nd))
                {
                    if (nd < today) status = "Overdue";
                    else if (nd <= today.AddDays(30)) status = "Due soon";
                }
                else
                {
                    status = "No next date";
                }

                grid.Rows.Add(
                    v.VaccinationID,
                    v.PetName,
                    v.OwnerName,
                    v.DateGiven,
                    string.IsNullOrWhiteSpace(v.NextDueDate) ? "—" : v.NextDueDate,
                    status,
                    "—");
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var petMgr = new PetManager();
            ARShotForm.BridgeLoadOwnerPets = () =>
            {
                var groups = petMgr.GetOwnerPetGroups();
                return groups.Select(g => new OwnerPetBridgeGroup
                {
                    DisplayLabel = g.DisplayLabel,
                    Pets = g.Pets.Select(p => new PetBridgeOption { PetId = p.PetId, Name = p.PetName }).ToList()
                }).ToList();
            };
            ARShotForm.BridgeSaveVaccination = (petId, given, next, notes) =>
                _vacMgr.AddVaccination(petId, given, next, notes);

            try
            {
                using var dialog = new ARShotForm();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    ReloadAll();
            }
            finally
            {
                ARShotForm.BridgeLoadOwnerPets = null;
                ARShotForm.BridgeSaveVaccination = null;
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Index < 0 || grid.CurrentRow.Index >= _rows.Count)
            {
                MessageBox.Show("Select a vaccination row to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = _rows[grid.CurrentRow.Index];
            if (MessageBox.Show($"Delete vaccination #{row.VaccinationID} for {row.PetName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_vacMgr.DeleteVaccination(row.VaccinationID))
                ReloadAll();
        }
    }
}
