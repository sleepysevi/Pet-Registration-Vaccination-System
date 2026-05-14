namespace AlagaTrack
{
    partial class LoginPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
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
            pictureBox1.Location = new Point(201, 101);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(598, 143);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LOGIN
            // 
            LOGIN.AutoSize = true;
            LOGIN.Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Bold, GraphicsUnit.Pixel);
            LOGIN.ForeColor = Color.FromArgb(32, 47, 124);
            LOGIN.Location = new Point(448, 244);
            LOGIN.Name = "LOGIN";
            LOGIN.Size = new Size(112, 36);
            LOGIN.TabIndex = 1;
            LOGIN.Text = "LOGIN";
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            Username.Location = new Point(367, 280);
            Username.Name = "Username";
            Username.Size = new Size(83, 20);
            Username.TabIndex = 3;
            Username.Text = "Username";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(61, 4);
            // 
            // EnterUsernamePanel
            // 
            EnterUsernamePanel.BackColor = Color.FromArgb(217, 217, 217);
            EnterUsernamePanel.Controls.Add(EnterUsernameTextBox);
            EnterUsernamePanel.Location = new Point(361, 300);
            EnterUsernamePanel.Name = "EnterUsernamePanel";
            EnterUsernamePanel.Size = new Size(279, 45);
            EnterUsernamePanel.TabIndex = 7;
            // 
            // EnterUsernameTextBox
            // 
            EnterUsernameTextBox.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular);
            EnterUsernameTextBox.BackColor = Color.FromArgb(217, 217, 217);
            EnterUsernameTextBox.Location = new Point(12, 13);
            EnterUsernameTextBox.Name = "EnterUsernameTextBox";
            EnterUsernameTextBox.Size = new Size(279, 55);
            EnterUsernameTextBox.TabIndex = 0;
            EnterUsernameTextBox.TextAlign = HorizontalAlignment.Left;
            EnterUsernameTextBox.BorderStyle = BorderStyle.None;

            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            Password.Location = new Point(367, 351);
            Password.Name = "Password";
            Password.Size = new Size(78, 20);
            Password.TabIndex = 8;
            Password.Text = "Password";
            // 
            // EnterPasswordPanel
            // 
            EnterPasswordPanel.BackColor = Color.FromArgb(217, 217, 217);
            EnterPasswordPanel.Controls.Add(EnterPasswordTextBox);
            EnterPasswordPanel.Location = new Point(361, 371);
            EnterPasswordPanel.Name = "EnterPasswordPanel";
            EnterPasswordPanel.Size = new Size(279, 45);
            EnterPasswordPanel.TabIndex = 9;
            // 
            // EnterPasswordTextBox
            // 
            EnterPasswordTextBox.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular);
            EnterPasswordTextBox.BackColor = Color.FromArgb(217, 217, 217);
            EnterPasswordTextBox.Location = new Point(12, 13);
            EnterPasswordTextBox.Name = "EnterPasswordTextBox";
            EnterPasswordTextBox.Size = new Size(279, 55);
            EnterPasswordTextBox.TabIndex = 0;
            EnterPasswordTextBox.TextAlign = HorizontalAlignment.Left;
            EnterPasswordTextBox.BorderStyle = BorderStyle.None;

            // 
            // ForgotPassword
            // 
            ForgotPassword.AutoSize = true;
            ForgotPassword.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Underline, GraphicsUnit.Pixel);
            ForgotPassword.ForeColor = Color.FromArgb(10, 10, 10);
            ForgotPassword.Location = new Point(367, 420);
            ForgotPassword.Name = "ForgotPassword";
            ForgotPassword.Size = new Size(131, 18);
            ForgotPassword.TabIndex = 10;
            ForgotPassword.Text = "Forgot Password?";
            ForgotPassword.Cursor = Cursors.Hand;
            // 
            // LogIns
            // 
            LogIns.BackColor = Color.FromArgb(32, 47, 124);
            LogIns.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            LogIns.ForeColor = Color.WhiteSmoke;
            LogIns.Location = new Point(448, 448);
            LogIns.Name = "LogIns";
            LogIns.Size = new Size(104, 40);
            LogIns.TabIndex = 11;
            LogIns.Text = "LOGIN";
            LogIns.UseVisualStyleBackColor = false;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(LogIns);
            Controls.Add(ForgotPassword);
            Controls.Add(EnterPasswordPanel);
            Controls.Add(Password);
            Controls.Add(EnterUsernamePanel);
            Controls.Add(Username);
            Controls.Add(LOGIN);
            Controls.Add(pictureBox1);
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
        private Label ForgotPassword;
        private Button LogIns;
    }
}
