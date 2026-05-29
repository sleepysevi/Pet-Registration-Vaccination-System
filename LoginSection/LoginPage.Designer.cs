namespace AlagaTrack
{
    partial class LoginPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            LOGIN = new Label();
            Username = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            contextMenuStrip2 = new ContextMenuStrip(components);
            EnterUsernamePanel = new Panel();
            EnterUsernameTextBox = new TextBox();
            Password = new Label();
            EnterPasswordPanel = new Panel();
            EnterPasswordTextBox = new TextBox();
            ForgotPassword = new Label();
            LogIns = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            EnterUsernamePanel.SuspendLayout();
            EnterPasswordPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::AlagaTrack.LoginSection.Properties.Resources.OriginalAlagaTrackLogo;
            pictureBox1.Location = new Point(192, 72);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 120);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LOGIN
            // 
            LOGIN.AutoSize = true;
            LOGIN.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            LOGIN.ForeColor = Color.FromArgb(32, 47, 124);
            LOGIN.Location = new Point(436, 200);
            LOGIN.Name = "LOGIN";
            LOGIN.TabIndex = 1;
            LOGIN.Text = "LOGIN";
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Font = new Font("Segoe UI", 10F);
            Username.Location = new Point(342, 248);
            Username.Name = "Username";
            Username.Text = "Username";
            // 
            // EnterUsernamePanel
            // 
            EnterUsernamePanel.BackColor = Color.FromArgb(243, 244, 246);
            EnterUsernamePanel.Controls.Add(EnterUsernameTextBox);
            EnterUsernamePanel.Location = new Point(342, 270);
            EnterUsernamePanel.Name = "EnterUsernamePanel";
            EnterUsernamePanel.Size = new Size(300, 44);
            // 
            // EnterUsernameTextBox
            // 
            EnterUsernameTextBox.BackColor = Color.FromArgb(243, 244, 246);
            EnterUsernameTextBox.BorderStyle = BorderStyle.None;
            EnterUsernameTextBox.Font = new Font("Segoe UI", 11F);
            EnterUsernameTextBox.Location = new Point(12, 11);
            EnterUsernameTextBox.Name = "EnterUsernameTextBox";
            EnterUsernameTextBox.Size = new Size(276, 27);
            EnterUsernameTextBox.TabIndex = 0;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Font = new Font("Segoe UI", 10F);
            Password.Location = new Point(342, 322);
            Password.Name = "Password";
            Password.Text = "Password";
            // 
            // EnterPasswordPanel
            // 
            EnterPasswordPanel.BackColor = Color.FromArgb(243, 244, 246);
            EnterPasswordPanel.Controls.Add(EnterPasswordTextBox);
            EnterPasswordPanel.Controls.Add(ShowPasswordButton);
            EnterPasswordPanel.Location = new Point(342, 344);
            EnterPasswordPanel.Name = "EnterPasswordPanel";
            EnterPasswordPanel.Size = new Size(300, 44);
            // 
            // EnterPasswordTextBox
            // 
            EnterPasswordTextBox.BackColor = Color.FromArgb(243, 244, 246);
            EnterPasswordTextBox.BorderStyle = BorderStyle.None;
            EnterPasswordTextBox.Font = new Font("Segoe UI", 11F);
            EnterPasswordTextBox.Location = new Point(12, 11);
            EnterPasswordTextBox.Name = "EnterPasswordTextBox";
            EnterPasswordTextBox.Size = new Size(210, 27);
            EnterPasswordTextBox.TabIndex = 0;
            EnterPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // ShowPasswordButton
            // 
            ShowPasswordButton = new Button();
            ShowPasswordButton.BackColor = Color.FromArgb(243, 244, 246);
            ShowPasswordButton.FlatAppearance.BorderSize = 0;
            ShowPasswordButton.FlatStyle = FlatStyle.Flat;
            ShowPasswordButton.Font = new Font("Segoe UI", 9F);
            ShowPasswordButton.ForeColor = Color.FromArgb(32, 47, 124);
            ShowPasswordButton.Location = new Point(228, 9);
            ShowPasswordButton.Name = "ShowPasswordButton";
            ShowPasswordButton.Size = new Size(60, 26);
            ShowPasswordButton.TabIndex = 12;
            ShowPasswordButton.Text = "Show";
            ShowPasswordButton.UseVisualStyleBackColor = false;
            ShowPasswordButton.Click += ShowPasswordButton_Click;
            // 
            // ForgotPassword
            // 
            ForgotPassword.AutoSize = true;
            ForgotPassword.Cursor = Cursors.Hand;
            ForgotPassword.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            ForgotPassword.ForeColor = Color.FromArgb(71, 85, 105);
            ForgotPassword.Location = new Point(511, 396);
            ForgotPassword.Name = "ForgotPassword";
            ForgotPassword.Text = "Forgot Password?";
            // 
            // LogIns
            // 
            LogIns.BackColor = Color.FromArgb(32, 47, 124);
            LogIns.FlatStyle = FlatStyle.Flat;
            LogIns.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            LogIns.ForeColor = Color.White;
            LogIns.Location = new Point(342, 424);
            LogIns.Name = "LogIns";
            LogIns.Size = new Size(300, 44);
            LogIns.TabIndex = 11;
            LogIns.Text = "LOGIN";
            LogIns.UseVisualStyleBackColor = false;
            // 
            // LoginPage
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(984, 561);
            Controls.Add(pictureBox1);
            Controls.Add(LOGIN);
            Controls.Add(Username);
            Controls.Add(EnterUsernamePanel);
            Controls.Add(Password);
            Controls.Add(EnterPasswordPanel);
            Controls.Add(ForgotPassword);
            Controls.Add(LogIns);
            Name = "LoginPage";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            EnterUsernamePanel.ResumeLayout(false);
            EnterUsernamePanel.PerformLayout();
            EnterPasswordPanel.ResumeLayout(false);
            EnterPasswordPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label LOGIN;
        private Label Username;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private Panel EnterUsernamePanel;
        private TextBox EnterUsernameTextBox;
        private Label Password;
        private Panel EnterPasswordPanel;
        private TextBox EnterPasswordTextBox;
        private Button ShowPasswordButton;
        private Label ForgotPassword;
        private Button LogIns;
    }
}
