using System;
using System.Windows.Forms;
using MainPages;

namespace AlagaTrackFrontEnd
{
    // Main shell form: hosts sidebar + active content page.
    public partial class Form1 : Form
    {
        private const int SidebarCollapsedWidth = 0;
        private const int SidebarExpandedWidth = 250;
        /// <summary>When true, the timer is shrinking the sidebar; when false, expanding. (Name kept for minimal diff.)</summary>
        private bool _isSidebarExpanded;
        private Form _activePage;
        private SidebarForm _sidebarForm;

        /// <summary>Milliseconds between width steps (same for open and close).</summary>
        private const int SidebarAnimationIntervalMs = 10;
        /// <summary>Pixels added/removed each tick (same for open and close).</summary>
        private const int SidebarAnimationStepPx = 25;

        public Form1()
        {
            InitializeComponent();
            sidebarTimer.Interval = SidebarAnimationIntervalMs;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start with collapsed sidebar for cleaner initial layout.
            panelSidebar.Width = SidebarCollapsedWidth;
            LoadSidebarForm();
            UpdateSidebarShape();
            panelSidebar.BringToFront();
            OpenPage(new MainPages.Dashboard(), "Dashboard");
        }

        private void LoadSidebarForm()
        {
            // Embed sidebar as a child form so all navigation stays centralized.
            _sidebarForm = new SidebarForm();
            _sidebarForm.TopLevel = false;
            _sidebarForm.FormBorderStyle = FormBorderStyle.None;
            _sidebarForm.Dock = DockStyle.Fill;
            _sidebarForm.NavigationRequested += NavigationRequested;
            _sidebarForm.HideSidebarRequested += CollapseSidebar;
            panelSidebar.Controls.Add(_sidebarForm);
            _sidebarForm.Show();
        }

        private void CollapseSidebar()
        {
            StartSidebarSlide(closing: true);
        }

        /// <summary>Same slide logic for hamburger toggle and in-sidebar hide — symmetric step/interval.</summary>
        private void StartSidebarSlide(bool closing)
        {
            if (closing)
            {
                if (panelSidebar.Width <= SidebarCollapsedWidth)
                    return;
            }
            else
            {
                if (panelSidebar.Width >= SidebarExpandedWidth)
                    return;
            }

            _isSidebarExpanded = closing;
            if (!sidebarTimer.Enabled)
                sidebarTimer.Start();
        }

        private void OpenPage(Form page, string title)
        {
            // Dispose old page to prevent hidden form instances from accumulating.
            if (_activePage != null)
            {
                panelContentHost.Controls.Remove(_activePage);
                _activePage.Dispose();
            }

            _activePage = page;
            page.TopLevel = false;
            page.FormBorderStyle = FormBorderStyle.None;
            page.Dock = DockStyle.Fill;
            panelContentHost.Controls.Add(page);
            page.Show();
            // Reflect active page in the label + window title.
            lblPageTitle.Text = title;
            Text = "AlagaTrack - " + title;
        }

        public void Navigate(string pageKey) => NavigationRequested(pageKey);

        private void NavigationRequested(string pageKey)
        {
            // Bisaya: diri ta mo-abli sa laing Form depende sa gipislit nga menu.
            switch (pageKey)
            {
                case "Dashboard":
                    OpenPage(new MainPages.Dashboard(), "Dashboard");
                    break;
                case "About":
                    OpenPage(new MainPages.AboutPage(), "About");
                    break;
                case "Owners":
                    OpenPage(new OwnersForm(), "Owners");
                    break;
                case "Pets":
                    OpenPage(new PetsForm(), "Pets");
                    break;
                case "Vaccinations":
                    OpenPage(new VaccinationsForm(), "Vaccinations");
                    break;
                case "Lost Pets":
                    OpenPage(new LostPetsForm(), "Lost Pets");
                    break;
                case "Staff Management":
                    OpenPage(new StaffManagementForm(), "Staff Management");
                    break;
                case "Logout":
                    // Placeholder only: logout flow will be added later.
                    break;
                default:
                    OpenPage(new OwnersForm(), "Owners");
                    break;
            }
        }

        private void btnHamburger_Click(object sender, EventArgs e)
        {
            // Toggle: past half width → animate closed; otherwise open (same rule as mid-animation reverse).
            bool closing = panelSidebar.Width * 2 > SidebarExpandedWidth;
            StartSidebarSlide(closing);
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            SuspendLayout();
            panelSidebar.SuspendLayout();
            try
            {
                if (_isSidebarExpanded)
                {
                    int w = panelSidebar.Width - SidebarAnimationStepPx;
                    if (w <= SidebarCollapsedWidth)
                    {
                        panelSidebar.Width = SidebarCollapsedWidth;
                        _isSidebarExpanded = false;
                        sidebarTimer.Stop();
                    }
                    else
                    {
                        panelSidebar.Width = w;
                    }
                }
                else
                {
                    int w = panelSidebar.Width + SidebarAnimationStepPx;
                    if (w >= SidebarExpandedWidth)
                    {
                        panelSidebar.Width = SidebarExpandedWidth;
                        _isSidebarExpanded = true;
                        sidebarTimer.Stop();
                    }
                    else
                    {
                        panelSidebar.Width = w;
                    }
                }

                UpdateSidebarShape();
                panelSidebar.BringToFront();
            }
            finally
            {
                panelSidebar.ResumeLayout();
                ResumeLayout(false);
            }
        }

        private void UpdateSidebarShape()
        {
            // Sidebar corners intentionally straight (not rounded).
            panelSidebar.Region = null;
        }

        private void panelContentHost_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPageTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
