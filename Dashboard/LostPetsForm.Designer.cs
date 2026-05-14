namespace AlagaTrackFrontEnd
{
    partial class LostPetsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.Panel card4;
        private System.Windows.Forms.Panel actionBar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCloseCase;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel tablePanel;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.DataGridView grid;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.card1 = new System.Windows.Forms.Panel();
            this.lblCard1Title = new System.Windows.Forms.Label();
            this.lblCard1Value = new System.Windows.Forms.Label();
            this.lblCard1Sub = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.lblCard2Title = new System.Windows.Forms.Label();
            this.lblCard2Value = new System.Windows.Forms.Label();
            this.lblCard2Sub = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.lblCard3Title = new System.Windows.Forms.Label();
            this.lblCard3Value = new System.Windows.Forms.Label();
            this.lblCard3Sub = new System.Windows.Forms.Label();
            this.card4 = new System.Windows.Forms.Panel();
            this.lblCard4Title = new System.Windows.Forms.Label();
            this.lblCard4Value = new System.Windows.Forms.Label();
            this.lblCard4Sub = new System.Windows.Forms.Label();
            this.actionBar = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCloseCase = new System.Windows.Forms.Button();
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
            this.card4.SuspendLayout();
            this.actionBar.SuspendLayout();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card1.Controls.Add(this.lblCard1Title);
            this.card1.Controls.Add(this.lblCard1Value);
            this.card1.Controls.Add(this.lblCard1Sub);
            this.card1.Location = new System.Drawing.Point(34, 54);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(229, 126);
            this.card1.TabIndex = 1;
            this.card1.Paint += new System.Windows.Forms.PaintEventHandler(this.card1_Paint);
            // 
            // lblCard1Title
            // 
            this.lblCard1Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard1Title.Location = new System.Drawing.Point(20, 6);
            this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(180, 24);
            this.lblCard1Title.TabIndex = 0;
            this.lblCard1Title.Text = "Active Lost";
            this.lblCard1Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard1Value
            // 
            this.lblCard1Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard1Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblCard1Value.Location = new System.Drawing.Point(19, 24);
            this.lblCard1Value.Name = "lblCard1Value";
            this.lblCard1Value.Size = new System.Drawing.Size(180, 61);
            this.lblCard1Value.TabIndex = 1;
            this.lblCard1Value.Text = "12";
            this.lblCard1Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard1Sub
            // 
            this.lblCard1Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblCard1Sub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard1Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblCard1Sub.Location = new System.Drawing.Point(18, 87);
            this.lblCard1Sub.Name = "lblCard1Sub";
            this.lblCard1Sub.Size = new System.Drawing.Size(180, 24);
            this.lblCard1Sub.TabIndex = 2;
            this.lblCard1Sub.Text = "Currently missing";
            this.lblCard1Sub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card2.Controls.Add(this.lblCard2Title);
            this.card2.Controls.Add(this.lblCard2Value);
            this.card2.Controls.Add(this.lblCard2Sub);
            this.card2.Location = new System.Drawing.Point(274, 54);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(229, 126);
            this.card2.TabIndex = 2;
            this.card2.Paint += new System.Windows.Forms.PaintEventHandler(this.card2_Paint);
            // 
            // lblCard2Title
            // 
            this.lblCard2Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard2Title.Location = new System.Drawing.Point(21, 6);
            this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(180, 24);
            this.lblCard2Title.TabIndex = 0;
            this.lblCard2Title.Text = "Found";
            this.lblCard2Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard2Value
            // 
            this.lblCard2Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard2Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCard2Value.Location = new System.Drawing.Point(21, 24);
            this.lblCard2Value.Name = "lblCard2Value";
            this.lblCard2Value.Size = new System.Drawing.Size(180, 61);
            this.lblCard2Value.TabIndex = 1;
            this.lblCard2Value.Text = "5";
            this.lblCard2Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard2Sub
            // 
            this.lblCard2Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblCard2Sub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard2Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCard2Sub.Location = new System.Drawing.Point(24, 88);
            this.lblCard2Sub.Name = "lblCard2Sub";
            this.lblCard2Sub.Size = new System.Drawing.Size(180, 24);
            this.lblCard2Sub.TabIndex = 2;
            this.lblCard2Sub.Text = "Recently located";
            this.lblCard2Sub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card3.Controls.Add(this.lblCard3Title);
            this.card3.Controls.Add(this.lblCard3Value);
            this.card3.Controls.Add(this.lblCard3Sub);
            this.card3.Location = new System.Drawing.Point(514, 54);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(229, 126);
            this.card3.TabIndex = 3;
            this.card3.Paint += new System.Windows.Forms.PaintEventHandler(this.card3_Paint);
            // 
            // lblCard3Title
            // 
            this.lblCard3Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard3Title.Location = new System.Drawing.Point(21, 6);
            this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(180, 24);
            this.lblCard3Title.TabIndex = 0;
            this.lblCard3Title.Text = "Recovered";
            this.lblCard3Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard3Value
            // 
            this.lblCard3Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard3Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCard3Value.Location = new System.Drawing.Point(19, 24);
            this.lblCard3Value.Name = "lblCard3Value";
            this.lblCard3Value.Size = new System.Drawing.Size(180, 61);
            this.lblCard3Value.TabIndex = 1;
            this.lblCard3Value.Text = "3";
            this.lblCard3Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard3Sub
            // 
            this.lblCard3Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblCard3Sub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard3Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblCard3Sub.Location = new System.Drawing.Point(23, 87);
            this.lblCard3Sub.Name = "lblCard3Sub";
            this.lblCard3Sub.Size = new System.Drawing.Size(180, 24);
            this.lblCard3Sub.TabIndex = 2;
            this.lblCard3Sub.Text = "Back home";
            this.lblCard3Sub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // card4
            // 
            this.card4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.card4.Controls.Add(this.lblCard4Title);
            this.card4.Controls.Add(this.lblCard4Value);
            this.card4.Controls.Add(this.lblCard4Sub);
            this.card4.Location = new System.Drawing.Point(754, 54);
            this.card4.Name = "card4";
            this.card4.Size = new System.Drawing.Size(229, 126);
            this.card4.TabIndex = 6;
            this.card4.Paint += new System.Windows.Forms.PaintEventHandler(this.card4_Paint);
            // 
            // lblCard4Title
            // 
            this.lblCard4Title.BackColor = System.Drawing.Color.Transparent;
            this.lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(139)))));
            this.lblCard4Title.Location = new System.Drawing.Point(28, 6);
            this.lblCard4Title.Name = "lblCard4Title";
            this.lblCard4Title.Size = new System.Drawing.Size(180, 24);
            this.lblCard4Title.TabIndex = 0;
            this.lblCard4Title.Text = "Recovery Rate";
            this.lblCard4Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard4Value
            // 
            this.lblCard4Value.BackColor = System.Drawing.Color.Transparent;
            this.lblCard4Value.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Bold);
            this.lblCard4Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCard4Value.Location = new System.Drawing.Point(23, 24);
            this.lblCard4Value.Name = "lblCard4Value";
            this.lblCard4Value.Size = new System.Drawing.Size(180, 61);
            this.lblCard4Value.TabIndex = 1;
            this.lblCard4Value.Text = "72%";
            this.lblCard4Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCard4Sub
            // 
            this.lblCard4Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblCard4Sub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCard4Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCard4Sub.Location = new System.Drawing.Point(24, 88);
            this.lblCard4Sub.Name = "lblCard4Sub";
            this.lblCard4Sub.Size = new System.Drawing.Size(180, 24);
            this.lblCard4Sub.TabIndex = 2;
            this.lblCard4Sub.Text = "Found/Total";
            this.lblCard4Sub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionBar
            // 
            this.actionBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.actionBar.Controls.Add(this.btnAdd);
            this.actionBar.Controls.Add(this.btnRefresh);
            this.actionBar.Controls.Add(this.btnCloseCase);
            this.actionBar.Controls.Add(this.txtSearch);
            this.actionBar.Location = new System.Drawing.Point(34, 180);
            this.actionBar.Name = "actionBar";
            this.actionBar.Size = new System.Drawing.Size(930, 46);
            this.actionBar.TabIndex = 4;
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
            this.btnAdd.Text = "+ Report Lost";
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
            // btnCloseCase
            // 
            this.btnCloseCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.btnCloseCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseCase.ForeColor = System.Drawing.Color.White;
            this.btnCloseCase.Location = new System.Drawing.Point(267, 6);
            this.btnCloseCase.Name = "btnCloseCase";
            this.btnCloseCase.Size = new System.Drawing.Size(145, 30);
            this.btnCloseCase.TabIndex = 2;
            this.btnCloseCase.Text = "Close Case";
            this.btnCloseCase.UseVisualStyleBackColor = false;
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
            this.txtSearch.Text = "Search pets...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // tablePanel
            // 
            this.tablePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tablePanel.Controls.Add(this.lblTable);
            this.tablePanel.Controls.Add(this.grid);
            this.tablePanel.Location = new System.Drawing.Point(12, 233);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(970, 312);
            this.tablePanel.TabIndex = 5;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.BackColor = System.Drawing.Color.Transparent;
            this.lblTable.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(56)))), ((int)(((byte)(74)))));
            this.lblTable.Location = new System.Drawing.Point(317, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(362, 65);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "Lost Pets Table";
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
            this.grid.Location = new System.Drawing.Point(24, 74);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 62;
            this.grid.Size = new System.Drawing.Size(930, 226);
            this.grid.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "PET";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "OWNER";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "LOST DATE";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "LOCATION";
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
            // LostPetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(978, 544);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.actionBar);
            this.Controls.Add(this.card4);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "LostPetsForm";
            this.Text = "LostPetsForm";
            this.card1.ResumeLayout(false);
            this.card2.ResumeLayout(false);
            this.card3.ResumeLayout(false);
            this.card4.ResumeLayout(false);
            this.actionBar.ResumeLayout(false);
            this.actionBar.PerformLayout();
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label lblCard1Title;
        private System.Windows.Forms.Label lblCard1Value;
        private System.Windows.Forms.Label lblCard1Sub;
        private System.Windows.Forms.Label lblCard2Title;
        private System.Windows.Forms.Label lblCard2Value;
        private System.Windows.Forms.Label lblCard2Sub;
        private System.Windows.Forms.Label lblCard3Title;
        private System.Windows.Forms.Label lblCard3Value;
        private System.Windows.Forms.Label lblCard3Sub;
        private System.Windows.Forms.Label lblCard4Title;
        private System.Windows.Forms.Label lblCard4Value;
        private System.Windows.Forms.Label lblCard4Sub;
    }
}

