using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AlagaTrack;
using MiniFrames;

namespace AlagaTrackFrontEnd
{
    public partial class StaffManagementForm : Form
    {
        private readonly StaffManager _mgr = new StaffManager();
        private List<StaffRecord> _rows = new List<StaffRecord>();

        public StaffManagementForm()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnRefresh.Click += (_, _) => ReloadAll();
            btnReset.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
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
            ReloadAll();
        }

        private void card1_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card3_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void card2_Paint(object sender, PaintEventArgs e)
        {
            UiRoundHelper.DrawCardShadow(sender as Panel, e);
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search staff...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void ReloadAll()
        {
            var all = _mgr.GetStaff(null);
            lblCard1Value.Text = all.Count.ToString("00");
            lblCard2Value.Text = all.Count(s => string.Equals(s.Role, "Admin", StringComparison.OrdinalIgnoreCase)).ToString("00");
            lblCard3Value.Text = all.Count(s => !string.Equals(s.Role, "Admin", StringComparison.OrdinalIgnoreCase)).ToString("00");

            var q = txtSearch.Text?.Trim() ?? "";
            if (q.Equals("Search staff...", StringComparison.OrdinalIgnoreCase))
                q = "";
            LoadGrid(q);
        }

        private void LoadGrid(string? search)
        {
            _rows = _mgr.GetStaff(string.IsNullOrWhiteSpace(search) ? null : search);
            grid.Rows.Clear();
            foreach (var s in _rows)
            {
                grid.Rows.Add(
                    s.Username,
                    s.Role,
                    string.IsNullOrWhiteSpace(s.AtPin) ? "—" : s.AtPin,
                    "—",
                    "Active",
                    "—");
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            StaffForm.BridgeSaveStaff = (u, name, role, phone, email, pin) =>
                _mgr.AddStaff(new StaffRecord
                {
                    Username = u,
                    FullName = name,
                    Role = role,
                    Phone = phone,
                    Email = email,
                    AtPin = pin
                });

            try
            {
                using var dialog = new StaffForm();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    ReloadAll();
            }
            finally
            {
                StaffForm.BridgeSaveStaff = null;
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Index < 0 || grid.CurrentRow.Index >= _rows.Count)
            {
                MessageBox.Show("Select a staff row to remove.", "Staff", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var s = _rows[grid.CurrentRow.Index];
            if (MessageBox.Show($"Remove staff '{s.Username}' from the list?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_mgr.DeleteStaff(s.StaffID))
                ReloadAll();
        }
    }
}
