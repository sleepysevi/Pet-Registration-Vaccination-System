using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AlagaTrack;
using MiniFrames;

namespace AlagaTrackFrontEnd
{
    public partial class LostPetsForm : Form
    {
        private readonly LostReportManager _mgr = new LostReportManager();
        private List<LostReportRecord> _rows = new List<LostReportRecord>();

        public LostPetsForm()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnRefresh.Click += (_, _) => ReloadAll();
            btnCloseCase.Click += BtnCloseCase_Click;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (UiRoundHelper.IsInDesignMode(this)) return;
            UiRoundHelper.ApplyDataViewChrome(this);

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

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search pets...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void ReloadAll()
        {
            int active = _mgr.CountByStatus("active");
            int found = _mgr.CountByStatus("found");
            int recovered = _mgr.CountByStatus("recovered");
            int total = _mgr.GetTotalReports();
            int closed = found + recovered;
            int rate = total <= 0 ? 0 : (int)Math.Round(100.0 * closed / total);

            lblCard1Value.Text = active.ToString();
            lblCard2Value.Text = found.ToString();
            lblCard3Value.Text = recovered.ToString();
            lblCard4Value.Text = $"{rate}%";

            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search pets...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void LoadGrid(string? search)
        {
            _rows = _mgr.GetReports(string.IsNullOrWhiteSpace(search) ? null : search);
            grid.Rows.Clear();
            foreach (var r in _rows)
            {
                grid.Rows.Add(
                    r.ReportID,
                    r.PetName,
                    r.OwnerName,
                    r.DateLost,
                    r.LastSeenLocation,
                    r.Status);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var petMgr = new PetManager();
            ReportLostForm.BridgeLoadOwnerPets = () =>
            {
                var groups = petMgr.GetOwnerPetGroups();
                return groups.Select(g => new OwnerPetBridgeGroup
                {
                    DisplayLabel = g.DisplayLabel,
                    Pets = g.Pets.Select(p => new PetBridgeOption { PetId = p.PetId, Name = p.PetName }).ToList()
                }).ToList();
            };
            ReportLostForm.BridgeSaveLostReport = (owner, pet, lostAt, loc, desc) =>
            {
                var rec = new LostReportRecord
                {
                    OwnerName = owner,
                    PetName = pet,
                    DateLost = lostAt.ToString("yyyy-MM-dd HH:mm"),
                    LastSeenLocation = loc,
                    Description = desc
                };
                return _mgr.AddReport(rec);
            };

            try
            {
                using var dialog = new ReportLostForm();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    ReloadAll();
            }
            finally
            {
                ReportLostForm.BridgeLoadOwnerPets = null;
                ReportLostForm.BridgeSaveLostReport = null;
            }
        }

        private void BtnCloseCase_Click(object? sender, EventArgs e)
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Index < 0 || grid.CurrentRow.Index >= _rows.Count)
            {
                MessageBox.Show("Select a report to close.", "Close case", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var r = _rows[grid.CurrentRow.Index];
            if (!string.Equals(r.Status, "active", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only active cases can be closed.", "Close case", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Mark report #{r.ReportID} as recovered?", "Close case", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (_mgr.SetStatus(r.ReportID, "recovered"))
                ReloadAll();
        }
    }
}
