using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AlagaTrack
{
    public partial class ForgotPasswordPage : Form
    {
        private readonly UserManager _userManager = new UserManager();

        public ForgotPasswordPage()
        {
            InitializeComponent();

            this.Load += ForgotPasswordPage_Load;
            this.Resize += ForgotPasswordPage_Resize;

            ChangePassword.FlatStyle = FlatStyle.Flat;
            ChangePassword.FlatAppearance.BorderSize = 0;
            ChangePassword.Paint += ChangePassword_Paint;
            ChangePassword.Click += ChangePassword_Click;
        }

        private void ForgotPasswordPage_Load(object sender, EventArgs e)
        {
            ApplyRounded();
        }

        private void ForgotPasswordPage_Resize(object sender, EventArgs e)
        {
            ApplyRounded();
        }

        private void ApplyRounded()
        {
            int radius = 18;

            SetRounded(UsernamePanel, radius);
            SetRounded(A_TPINCodePanel, radius);
            SetRounded(NewPasswordPanel, radius);
            SetRounded(Re_EnterPasswordPanel, radius);
            SetRounded(ChangePassword, radius);
        }

        private void SetRounded(Control c, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, c.Width, c.Height);
            c.Region = new Region(RoundedRect(bounds, radius));
        }

        private GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        // SHADOW 
        private void ChangePassword_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.Clear(btn.BackColor);

            string text = btn.Text;
            Font font = btn.Font;

            SizeF textSize = e.Graphics.MeasureString(text, font);

            float x = (btn.Width - textSize.Width) / 2;
            float y = (btn.Height - textSize.Height) / 2;

            // shadow
            using (Brush shadow = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
            {
                e.Graphics.DrawString(text, font, shadow, x + 2, y + 2);
            }

            // main text
            using (Brush brush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(text, font, brush, x, y);
            }
        }

        private void ChangePassword_Click(object? sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string pinCode = A_TPINCODETextBox.Text.Trim();
            string newPassword = NewPasswordTextBox.Text;
            string reEnterPassword = Re_EnterPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(pinCode) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(reEnterPassword))
            {
                MessageBox.Show("Please fill out all fields.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(newPassword, reEnterPassword, StringComparison.Ordinal))
            {
                MessageBox.Show("Passwords do not match.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_userManager.UserExists(username))
            {
                MessageBox.Show("Username not found.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_userManager.UpdatePassword(username, newPassword))
            {
                MessageBox.Show("Unable to change password.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Password changed successfully. Please log in.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            Hide();
        }
    }
}