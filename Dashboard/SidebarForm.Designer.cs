namespace AlagaTrackFrontEnd
{
    partial class SidebarForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Button btnLostPets;
        private System.Windows.Forms.Button btnVaccinations;
        private System.Windows.Forms.Button btnPets;
        private System.Windows.Forms.Button btnOwners;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel panelSidebarHeader;
        private System.Windows.Forms.Button btnSidebarHide;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnLostPets = new System.Windows.Forms.Button();
            this.btnVaccinations = new System.Windows.Forms.Button();
            this.btnPets = new System.Windows.Forms.Button();
            this.btnOwners = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelSidebarHeader = new System.Windows.Forms.Panel();
            this.btnSidebarHide = new System.Windows.Forms.Button();
            this.panelSidebar.SuspendLayout();
            this.panelSidebarHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.White;
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnStaff);
            this.panelSidebar.Controls.Add(this.btnLostPets);
            this.panelSidebar.Controls.Add(this.btnVaccinations);
            this.panelSidebar.Controls.Add(this.btnPets);
            this.panelSidebar.Controls.Add(this.btnOwners);
            this.panelSidebar.Controls.Add(this.btnAbout);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.panelSidebarHeader);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(321, 800);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLogout.Location = new System.Drawing.Point(0, 672);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(321, 75);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStaff.FlatAppearance.BorderSize = 0;
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnStaff.Location = new System.Drawing.Point(0, 407);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(4);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnStaff.Size = new System.Drawing.Size(321, 60);
            this.btnStaff.TabIndex = 1;
            this.btnStaff.Text = "Staff (Admin)";
            this.btnStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnLostPets
            // 
            this.btnLostPets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLostPets.FlatAppearance.BorderSize = 0;
            this.btnLostPets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLostPets.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLostPets.Location = new System.Drawing.Point(0, 347);
            this.btnLostPets.Margin = new System.Windows.Forms.Padding(4);
            this.btnLostPets.Name = "btnLostPets";
            this.btnLostPets.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnLostPets.Size = new System.Drawing.Size(321, 60);
            this.btnLostPets.TabIndex = 2;
            this.btnLostPets.Text = "Lost Pets";
            this.btnLostPets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLostPets.UseVisualStyleBackColor = true;
            this.btnLostPets.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnVaccinations
            // 
            this.btnVaccinations.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVaccinations.FlatAppearance.BorderSize = 0;
            this.btnVaccinations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVaccinations.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnVaccinations.Location = new System.Drawing.Point(0, 287);
            this.btnVaccinations.Margin = new System.Windows.Forms.Padding(4);
            this.btnVaccinations.Name = "btnVaccinations";
            this.btnVaccinations.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnVaccinations.Size = new System.Drawing.Size(321, 60);
            this.btnVaccinations.TabIndex = 3;
            this.btnVaccinations.Text = "Vaccinations";
            this.btnVaccinations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVaccinations.UseVisualStyleBackColor = true;
            this.btnVaccinations.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnPets
            // 
            this.btnPets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPets.FlatAppearance.BorderSize = 0;
            this.btnPets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPets.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnPets.Location = new System.Drawing.Point(0, 227);
            this.btnPets.Margin = new System.Windows.Forms.Padding(4);
            this.btnPets.Name = "btnPets";
            this.btnPets.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnPets.Size = new System.Drawing.Size(321, 60);
            this.btnPets.TabIndex = 4;
            this.btnPets.Text = "Pets";
            this.btnPets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPets.UseVisualStyleBackColor = true;
            this.btnPets.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnOwners
            // 
            this.btnOwners.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOwners.FlatAppearance.BorderSize = 0;
            this.btnOwners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOwners.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnOwners.Location = new System.Drawing.Point(0, 167);
            this.btnOwners.Margin = new System.Windows.Forms.Padding(4);
            this.btnOwners.Name = "btnOwners";
            this.btnOwners.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnOwners.Size = new System.Drawing.Size(321, 60);
            this.btnOwners.TabIndex = 5;
            this.btnOwners.Text = "Owners";
            this.btnOwners.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOwners.UseVisualStyleBackColor = true;
            this.btnOwners.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAbout.Location = new System.Drawing.Point(0, 167);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnAbout.Size = new System.Drawing.Size(321, 60);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.Text = "About";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDashboard.Location = new System.Drawing.Point(0, 107);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(321, 60);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.NavigationButton_Click);
            // 
            // panelSidebarHeader
            // 
            this.panelSidebarHeader.Controls.Add(this.btnSidebarHide);
            this.panelSidebarHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebarHeader.Location = new System.Drawing.Point(0, 0);
            this.panelSidebarHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelSidebarHeader.Name = "panelSidebarHeader";
            this.panelSidebarHeader.Size = new System.Drawing.Size(321, 107);
            this.panelSidebarHeader.TabIndex = 7;
            // 
            // btnSidebarHide
            // 
            this.btnSidebarHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSidebarHide.FlatAppearance.BorderSize = 0;
            this.btnSidebarHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSidebarHide.Image = global::AlagaTrackFrontEnd.Dashboard.Properties.Resources.Rectangle_6;
            this.btnSidebarHide.Location = new System.Drawing.Point(0, 0);
            this.btnSidebarHide.Margin = new System.Windows.Forms.Padding(4);
            this.btnSidebarHide.Name = "btnSidebarHide";
            this.btnSidebarHide.Size = new System.Drawing.Size(321, 107);
            this.btnSidebarHide.TabIndex = 0;
            this.btnSidebarHide.UseVisualStyleBackColor = true;
            this.btnSidebarHide.Click += new System.EventHandler(this.btnSidebarHide_Click);
            // 
            // SidebarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 800);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SidebarForm";
            this.Text = "SidebarForm";
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebarHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
