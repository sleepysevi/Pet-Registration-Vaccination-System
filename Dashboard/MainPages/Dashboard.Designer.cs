namespace MainPages
{
    partial class Dashboard
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox4 = new PictureBox();
            label1 = new Label();
            Header = new Panel();
            LastLogin = new Label();
            QuickStats = new Label();
            VaccinatedDash = new Panel();
            Vaccinated = new Label();
            TotalPetsDash = new Panel();
            TotalPets = new Label();
            LostDash = new Panel();
            Lost = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            Header.SuspendLayout();
            VaccinatedDash.SuspendLayout();
            TotalPetsDash.SuspendLayout();
            LostDash.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = AlagaTrackFrontEnd.Dashboard.MainPages.Properties.Resources.SmallOriginalAlagaTrackLogo;
            pictureBox1.Location = new Point(94, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(300, 72);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = AlagaTrackFrontEnd.Dashboard.MainPages.Properties.Resources.SideBarIcon;
            pictureBox2.Location = new Point(33, 32);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(55, 35);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = AlagaTrackFrontEnd.Dashboard.MainPages.Properties.Resources.WhiteLine;
            pictureBox4.Location = new Point(461, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(10, 76);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 2;
            pictureBox4.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Bold, GraphicsUnit.Pixel);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(29, 21);
            label1.Name = "label1";
            label1.Size = new Size(295, 32);
            label1.TabIndex = 1;
            label1.Text = "Welcome, Barangay!";
            // 
            // Header
            // 
            Header.Controls.Add(LastLogin);
            Header.Controls.Add(label1);
            Header.Controls.Add(pictureBox4);
            Header.Controls.Add(QuickStats);
            Header.Location = new Point(34, 81);
            Header.Name = "Header";
            Header.Size = new Size(932, 76);
            Header.TabIndex = 3;
            // 
            // LastLogin
            // 
            LastLogin.AutoSize = true;
            LastLogin.BackColor = Color.Transparent;
            LastLogin.Font = new Font("Microsoft Sans Serif", 26F, FontStyle.Bold, GraphicsUnit.Pixel);
            LastLogin.ForeColor = SystemColors.Control;
            LastLogin.Location = new Point(503, 11);
            LastLogin.Name = "LastLogin";
            LastLogin.Size = new Size(148, 30);
            LastLogin.TabIndex = 0;
            LastLogin.Text = "Last Login:";
            // 
            // QuickStats
            // 
            QuickStats.AutoSize = true;
            QuickStats.BackColor = Color.Transparent;
            QuickStats.Font = new Font("Microsoft Sans Serif", 26F, FontStyle.Bold, GraphicsUnit.Pixel);
            QuickStats.ForeColor = SystemColors.Control;
            QuickStats.Location = new Point(503, 34);
            QuickStats.Name = "QuickStats";
            QuickStats.Size = new Size(163, 30);
            QuickStats.TabIndex = 3;
            QuickStats.Text = "Quick Stats:";
            // 
            // VaccinatedDash
            // 
            VaccinatedDash.BackColor = Color.White;
            VaccinatedDash.Controls.Add(Vaccinated);
            VaccinatedDash.Location = new Point(362, 163);
            VaccinatedDash.Name = "VaccinatedDash";
            VaccinatedDash.Size = new Size(280, 120);
            VaccinatedDash.TabIndex = 2;
            // 
            // Vaccinated
            // 
            Vaccinated.AutoSize = true;
            Vaccinated.Font = new Font("Microsoft Sans Serif", 22F, FontStyle.Bold, GraphicsUnit.Pixel);
            Vaccinated.ForeColor = Color.FromArgb(100, 116, 139);
            Vaccinated.Location = new Point(77, 9);
            Vaccinated.Name = "Vaccinated";
            Vaccinated.Size = new Size(130, 26);
            Vaccinated.TabIndex = 0;
            Vaccinated.Text = "Vaccinated";
            // 
            // TotalPetsDash
            // 
            TotalPetsDash.BackColor = Color.White;
            TotalPetsDash.Controls.Add(TotalPets);
            TotalPetsDash.Location = new Point(66, 163);
            TotalPetsDash.Name = "TotalPetsDash";
            TotalPetsDash.Size = new Size(280, 120);
            TotalPetsDash.TabIndex = 1;
            // 
            // TotalPets
            // 
            TotalPets.AutoSize = true;
            TotalPets.Font = new Font("Microsoft Sans Serif", 22F, FontStyle.Bold, GraphicsUnit.Pixel);
            TotalPets.ForeColor = Color.FromArgb(100, 116, 139);
            TotalPets.Location = new Point(77, 9);
            TotalPets.Name = "TotalPets";
            TotalPets.Size = new Size(119, 26);
            TotalPets.TabIndex = 0;
            TotalPets.Text = "Total Pets";
            // 
            // LostDash
            // 
            LostDash.BackColor = Color.White;
            LostDash.Controls.Add(Lost);
            LostDash.Location = new Point(658, 163);
            LostDash.Name = "LostDash";
            LostDash.Size = new Size(280, 120);
            LostDash.TabIndex = 0;
            // 
            // Lost
            // 
            Lost.AutoSize = true;
            Lost.Font = new Font("Microsoft Sans Serif", 22F, FontStyle.Bold, GraphicsUnit.Pixel);
            Lost.ForeColor = Color.FromArgb(100, 116, 139);
            Lost.Location = new Point(105, 9);
            Lost.Name = "Lost";
            Lost.Size = new Size(57, 26);
            Lost.TabIndex = 0;
            Lost.Text = "Lost";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(984, 561);
            Controls.Add(LostDash);
            Controls.Add(TotalPetsDash);
            Controls.Add(VaccinatedDash);
            Controls.Add(Header);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AlagaTrack";
            Load += Dashboard_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            Header.ResumeLayout(false);
            Header.PerformLayout();
            VaccinatedDash.ResumeLayout(false);
            VaccinatedDash.PerformLayout();
            TotalPetsDash.ResumeLayout(false);
            TotalPetsDash.PerformLayout();
            LostDash.ResumeLayout(false);
            LostDash.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;

        private Label label1;
        private Panel Header;

        private Label LastLogin;
        private Label QuickStats;

        private Panel VaccinatedDash;
        private Panel TotalPetsDash;
        private Panel LostDash;

        private Label TotalPets;
        private Label Vaccinated;
        private Label Lost;
    }
}