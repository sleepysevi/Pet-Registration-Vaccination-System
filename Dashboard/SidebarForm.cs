using System;
using System.Windows.Forms;

namespace AlagaTrackFrontEnd
{
    // Sidebar navigation menu used inside Form1.
    public partial class SidebarForm : Form
    {
        public event Action<string> NavigationRequested;
        public event Action HideSidebarRequested;

        public SidebarForm()
        {
            InitializeComponent();
            SetupTags();
        }

        private void SetupTags()
        {
            // Tag values are consumed by Form1 navigation switch.
            btnDashboard.Tag = "Dashboard";
            btnAbout.Tag = "About";
            btnOwners.Tag = "Owners";
            btnPets.Tag = "Pets";
            btnVaccinations.Tag = "Vaccinations";
            btnLostPets.Tag = "Lost Pets";
            btnStaff.Tag = "Staff Management";
            btnLogout.Tag = "Logout";
        }

        private void NavigationButton_Click(object sender, EventArgs e)
        {
            // Notify main shell form which navigation item was clicked.
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null)
            {
                return;
            }

            if (NavigationRequested != null)
            {
                NavigationRequested(btn.Tag.ToString());
            }
        }

        private void btnSidebarHide_Click(object sender, EventArgs e)
        {
            if (HideSidebarRequested != null)
            {
                HideSidebarRequested();
            }
        }
    }
}
