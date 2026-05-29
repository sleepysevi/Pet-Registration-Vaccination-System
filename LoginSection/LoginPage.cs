using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlagaTrackFrontEnd;

namespace AlagaTrack
{
    public partial class LoginPage : Form
    {
        private readonly UserManager _userManager = new UserManager();
        private bool _usernameFocused;
        private bool _passwordFocused;

        public LoginPage()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            BackColor = UiTheme.AppBackground;
            DoubleBuffered = true;

            ForgotPassword.Cursor = Cursors.Hand;
            ForgotPassword.Click += ForgotPassword_Click;

            Shown += LoginPage_Shown;

            EnterUsernamePanel.Paint += EnterUsernamePanel_Paint;
            EnterPasswordPanel.Paint += EnterPasswordPanel_Paint;

            EnterUsernameTextBox.Enter += (_, _) => { _usernameFocused = true; EnterUsernamePanel.Invalidate(); };
            EnterUsernameTextBox.Leave += (_, _) => { _usernameFocused = false; EnterUsernamePanel.Invalidate(); };
            EnterPasswordTextBox.Enter += (_, _) => { _passwordFocused = true; EnterPasswordPanel.Invalidate(); };
            EnterPasswordTextBox.Leave += (_, _) => { _passwordFocused = false; EnterPasswordPanel.Invalidate(); };

            LogIns.MouseEnter += LogIns_MouseEnter;
            LogIns.MouseLeave += LogIns_MouseLeave;
            LogIns.Click += LogIns_Click;

            ApplyVisualDefaults();
        }

        private void ApplyVisualDefaults()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.SendToBack();

            EnterUsernamePanel.BackColor = UiTheme.InputFill;
            EnterPasswordPanel.BackColor = UiTheme.InputFill;
            EnterUsernameTextBox.BorderStyle = BorderStyle.None;
            EnterPasswordTextBox.BorderStyle = BorderStyle.None;
            EnterUsernameTextBox.BackColor = UiTheme.InputFill;
            EnterPasswordTextBox.BackColor = UiTheme.InputFill;
            EnterUsernameTextBox.Font = new Font("Segoe UI", 11F);
            EnterPasswordTextBox.Font = new Font("Segoe UI", 11F);

            ShowPasswordButton.FlatStyle = FlatStyle.Flat;
            ShowPasswordButton.FlatAppearance.BorderSize = 0;
            ShowPasswordButton.BackColor = UiTheme.InputFill;
            ShowPasswordButton.ForeColor = UiTheme.Primary;
            ShowPasswordButton.Font = new Font("Segoe UI", 9F);

            LogIns.FlatStyle = FlatStyle.Flat;
            LogIns.FlatAppearance.BorderSize = 0;
            LogIns.BackColor = UiTheme.Primary;
            LogIns.ForeColor = Color.White;
            LogIns.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            LogIns.Cursor = Cursors.Hand;

            LOGIN.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            LOGIN.ForeColor = UiTheme.Primary;
            Username.Font = Password.Font = new Font("Segoe UI", 10F);
            ForgotPassword.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            ForgotPassword.ForeColor = UiTheme.TextSecondary;
        }

        private void ForgotPassword_Click(object sender, EventArgs e)
        {
            var fp = new ForgotPasswordPage();
            fp.Show();
            Hide();
        }

        private void LoginPage_Shown(object sender, EventArgs e)
        {
            AlignLoginChrome();
        }

        private void AlignLoginChrome()
        {
            pictureBox1.SendToBack();

            const int fieldWidth = 300;
            int left = (ClientSize.Width - fieldWidth) / 2;

            EnterUsernamePanel.SetBounds(left, EnterUsernamePanel.Top, fieldWidth, 44);
            EnterPasswordPanel.SetBounds(left, EnterPasswordPanel.Top, fieldWidth, 44);
            Username.Left = left;
            Password.Left = left;

            ForgotPassword.Left = left + fieldWidth - ForgotPassword.PreferredWidth;
            LogIns.SetBounds(left, LogIns.Top, fieldWidth, 44);

            LOGIN.Left = (ClientSize.Width - LOGIN.PreferredWidth) / 2;
        }

        private void EnterUsernamePanel_Paint(object sender, PaintEventArgs e) =>
            PaintInputBorder(e, EnterUsernamePanel, _usernameFocused);

        private void EnterPasswordPanel_Paint(object sender, PaintEventArgs e) =>
            PaintInputBorder(e, EnterPasswordPanel, _passwordFocused);

        private static void PaintInputBorder(PaintEventArgs e, Panel panel, bool focused)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = panel.ClientRectangle;
            rect.Inflate(-1, -1);
            using var path = BuildRoundedPath(rect, UiTheme.InputRadius);
            using var fill = new SolidBrush(UiTheme.InputFill);
            using var pen = new Pen(focused ? UiTheme.Primary : UiTheme.Border, 1.5f) { Alignment = PenAlignment.Inset };
            e.Graphics.FillPath(fill, path);
            e.Graphics.DrawPath(pen, path);
        }

        private static GraphicsPath BuildRoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = Math.Min(radius * 2, Math.Min(rect.Width, rect.Height));
            if (d <= 0) return path;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void LogIns_MouseEnter(object sender, EventArgs e) => LogIns.BackColor = UiTheme.PrimaryHover;
        private void LogIns_MouseLeave(object sender, EventArgs e) => LogIns.BackColor = UiTheme.Primary;

        private void ShowPasswordButton_Click(object sender, EventArgs e)
        {
            EnterPasswordTextBox.UseSystemPasswordChar = !EnterPasswordTextBox.UseSystemPasswordChar;
            ShowPasswordButton.Text = EnterPasswordTextBox.UseSystemPasswordChar ? "Show" : "Hide";
        }

        private void LogIns_Click(object? sender, EventArgs e)
        {
            string username = EnterUsernameTextBox.Text.Trim();
            string password = EnterPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_userManager.AuthenticateUser(username, password, out int userId, out string role))
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserSession.UserID = userId;
            UserSession.Username = username;
            UserSession.Role = role;

            var dashboard = new Form1();
            dashboard.FormClosed += (_, _) => Close();
            dashboard.Show();
            Hide();
        }
    }
}
