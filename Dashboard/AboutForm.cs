using System.Drawing;
using System.Windows.Forms;

namespace AlagaTrackFrontEnd
{
    public class AboutForm : Form
    {
        public AboutForm()
        {
            BackColor = Color.FromArgb(238, 238, 238);
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;

            var title = new Label
            {
                Text = "About AlagaTrack",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 47, 127),
                AutoSize = true,
                Location = new Point(40, 30),
                BackColor = Color.Transparent
            };
            Controls.Add(title);

            var about = new Label
            {
                Text = "AlagaTrack is a barangay pet management system for owners, pets, vaccinations,\nand lost pet reporting.\n\nVersion: 1.0.0\nBuilt with Windows Forms + MySQL.",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(55, 65, 81),
                AutoSize = true,
                Location = new Point(44, 95),
                BackColor = Color.Transparent
            };
            Controls.Add(about);
        }
    }
}
