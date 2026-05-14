namespace AlagaTrackFrontEnd
{
    partial class StaffManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Panel actionBar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel tablePanel;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.DataGridView grid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.card1 = new System.Windows.Forms.Panel();
            this.lblCard1Value = new System.Windows.Forms.Label();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.lblCard2Value = new System.Windows.Forms.Label();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.lblCard3Value = new System.Windows.Forms.Label();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.actionBar = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tablePanel = new System.Windows.Forms.Panel();
            this.lblTable = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.actionBar.SuspendLayout();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card1.Controls.Add(this.lblCard1Value);
            this.card1.Controls.Add(this.lblCard1Title);
            this.card1.Location = new System.Drawing.Point(64, 55);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(280, 120);
            this.card1.TabIndex = 4;
            this.card1.Paint += new System.Windows.Forms.PaintEventHandler(this.card1_Paint);
            // 
            // lblCard1Value
            // 
            this.lblCard1Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard1Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblCard1Value.Location = new System.Drawing.Point(10, 31);
            this.lblCard1Value.Name = "lblCard1Value";
            this.lblCard1Value.Size = new System.Drawing.Size(240, 78);
            this.lblCard1Value.TabIndex = 1;
            this.lblCard1Value.Text = "00";
            this.lblCard1Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard1Title
            // 
            this.lblCard1Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard1Title.Location = new System.Drawing.Point(10, 6);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(240, 24);
            this.lblCard1Title.TabIndex = 0;
            this.lblCard1Title.Text = "Total Staff";
            this.lblCard1Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card2.Controls.Add(this.lblCard2Value);
            this.card2.Controls.Add(this.lblCard2Title);
            this.card2.Location = new System.Drawing.Point(360, 55);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(280, 120);
            this.card2.TabIndex = 3;
            this.card2.Paint += new System.Windows.Forms.PaintEventHandler(this.card2_Paint);
            // 
            // lblCard2Value
            // 
            this.lblCard2Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard2Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblCard2Value.Location = new System.Drawing.Point(10, 31);
            this.lblCard2Value.Name = "lblCard2Value";
            this.lblCard2Value.Size = new System.Drawing.Size(240, 78);
            this.lblCard2Value.TabIndex = 1;
            this.lblCard2Value.Text = "00";
            this.lblCard2Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard2Title
            // 
            this.lblCard2Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard2Title.Location = new System.Drawing.Point(10, 6);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(240, 24);
            this.lblCard2Title.TabIndex = 0;
            this.lblCard2Title.Text = "Active";
            this.lblCard2Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card3.Controls.Add(this.lblCard3Value);
            this.card3.Controls.Add(this.lblCard3Title);
            this.card3.Location = new System.Drawing.Point(656, 55);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(280, 120);
            this.card3.TabIndex = 2;
            this.card3.Paint += new System.Windows.Forms.PaintEventHandler(this.card3_Paint);
            // 
            // lblCard3Value
            // 
            this.lblCard3Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard3Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblCard3Value.Location = new System.Drawing.Point(10, 31);
            this.lblCard3Value.Name = "lblCard3Value";
            this.lblCard3Value.Size = new System.Drawing.Size(240, 78);
            this.lblCard3Value.TabIndex = 1;
            this.lblCard3Value.Text = "00";
            this.lblCard3Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard3Title
            // 
            this.lblCard3Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard3Title.Location = new System.Drawing.Point(10, 6);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(240, 24);
            this.lblCard3Title.TabIndex = 0;
            this.lblCard3Title.Text = "Logins 30d";
            this.lblCard3Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionBar
            // 
            this.actionBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.actionBar.Controls.Add(this.btnAdd);
            this.actionBar.Controls.Add(this.btnRefresh);
            this.actionBar.Controls.Add(this.btnReset);
            this.actionBar.Controls.Add(this.txtSearch);
            this.actionBar.Location = new System.Drawing.Point(34, 180);
            this.actionBar.Name = "actionBar";
            this.actionBar.Size = new System.Drawing.Size(930, 46);
            this.actionBar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(183)))), ((int)(((byte)(143)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(5, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Staff";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(118)))), ((int)(((byte)(141)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(141, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(267, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(145, 30);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Remove staff";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(505, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(341, 29);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "Search staff...";
            // 
            // tablePanel
            // 
            this.tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tablePanel.Controls.Add(this.lblTable);
            this.tablePanel.Controls.Add(this.grid);
            this.tablePanel.Location = new System.Drawing.Point(16, 234);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(968, 352);
            this.tablePanel.TabIndex = 0;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(56)))), ((int)(((byte)(74)))));
            this.lblTable.Location = new System.Drawing.Point(305, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(268, 65);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "Staff Table";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.grid.Location = new System.Drawing.Point(13, 74);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 62;
            this.grid.Size = new System.Drawing.Size(940, 264);
            this.grid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "USERNAME";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ROLE";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "AT-PIN";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "LAST LOGIN";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "STATUS";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "ACTION";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // StaffManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(978, 544);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.actionBar);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "StaffManagementForm";
            this.Text = "StaffManagementForm";
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.actionBar.ResumeLayout(false);
            this.actionBar.PerformLayout();
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblCard1Value;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblCard2Value;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblCard3Value;
    }
}

