using System.Drawing;
using System.Windows.Forms;

namespace AlagaTrackFrontEnd
{
    // Starter dashboard page with visible content.
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            BuildDashboardContent();
        }

        private void BuildDashboardContent()
        {
            var title = new Label
            {
                Text = "Welcome to AlagaTrack",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 47, 127),
                AutoSize = true,
                Location = new Point(40, 30),
                BackColor = Color.Transparent
            };
            Controls.Add(title);

            var subtitle = new Label
            {
                Text = "Manage owners, pets, vaccinations, and lost pet reports from the left menu.",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Location = new Point(44, 85),
                BackColor = Color.Transparent
            };
            Controls.Add(subtitle);

            var quickPanel = new Panel
            {
                BackColor = Color.WhiteSmoke,
                Size = new Size(900, 320),
                Location = new Point(40, 130)
            };
            Controls.Add(quickPanel);

            var quickTitle = new Label
            {
                Text = "Quick Start",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(42, 56, 74),
                AutoSize = true,
                Location = new Point(24, 18),
                BackColor = Color.Transparent
            };
            quickPanel.Controls.Add(quickTitle);

            var tips = new Label
            {
                Text = "1) Add an owner first\n2) Register pets under an owner\n3) Track vaccinations\n4) Use Lost Pets for recovery reports",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = Color.FromArgb(55, 65, 81),
                AutoSize = true,
                Location = new Point(28, 70),
                BackColor = Color.Transparent
            };
            quickPanel.Controls.Add(tips);
        }
    }
}
