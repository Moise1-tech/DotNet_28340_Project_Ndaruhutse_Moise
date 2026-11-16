namespace ScholarshipPRO_28340
{
    partial class AdminDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.showAllBtn = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.filterBtn = new System.Windows.Forms.Button();
            this.IncreaseTotalWithFilter = new System.Windows.Forms.Label();
            this.approveBtn = new System.Windows.Forms.Button();
            this.rejectBtn = new System.Windows.Forms.Button();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.adminDgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.adminDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(393, 248);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(282, 48);
            this.txtSearch.TabIndex = 32;
            // 
            // showAllBtn
            // 
            this.showAllBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.showAllBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showAllBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showAllBtn.Location = new System.Drawing.Point(159, 237);
            this.showAllBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.showAllBtn.Name = "showAllBtn";
            this.showAllBtn.Size = new System.Drawing.Size(225, 71);
            this.showAllBtn.TabIndex = 31;
            this.showAllBtn.Text = "SHOW ALL";
            this.showAllBtn.UseVisualStyleBackColor = false;
            this.showAllBtn.Click += new System.EventHandler(this.showAllBtn_Click);
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(832, 252);
            this.cmbFilterStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(306, 48);
            this.cmbFilterStatus.TabIndex = 30;
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
            // 
            // filterBtn
            // 
            this.filterBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.filterBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.filterBtn.Location = new System.Drawing.Point(1149, 246);
            this.filterBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(148, 71);
            this.filterBtn.TabIndex = 29;
            this.filterBtn.Text = "Filter";
            this.filterBtn.UseVisualStyleBackColor = false;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // IncreaseTotalWithFilter
            // 
            this.IncreaseTotalWithFilter.AutoSize = true;
            this.IncreaseTotalWithFilter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncreaseTotalWithFilter.Location = new System.Drawing.Point(1306, 262);
            this.IncreaseTotalWithFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IncreaseTotalWithFilter.Name = "IncreaseTotalWithFilter";
            this.IncreaseTotalWithFilter.Size = new System.Drawing.Size(252, 32);
            this.IncreaseTotalWithFilter.TabIndex = 28;
            this.IncreaseTotalWithFilter.Text = "Total Application. 0";
            // 
            // approveBtn
            // 
            this.approveBtn.BackColor = System.Drawing.Color.Blue;
            this.approveBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.approveBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.approveBtn.Location = new System.Drawing.Point(1372, 520);
            this.approveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.approveBtn.Name = "approveBtn";
            this.approveBtn.Size = new System.Drawing.Size(188, 71);
            this.approveBtn.TabIndex = 27;
            this.approveBtn.Text = "APPROVE";
            this.approveBtn.UseVisualStyleBackColor = false;
            this.approveBtn.Click += new System.EventHandler(this.approveBtn_Click);
            // 
            // rejectBtn
            // 
            this.rejectBtn.BackColor = System.Drawing.Color.Red;
            this.rejectBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rejectBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rejectBtn.Location = new System.Drawing.Point(1372, 660);
            this.rejectBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rejectBtn.Name = "rejectBtn";
            this.rejectBtn.Size = new System.Drawing.Size(188, 71);
            this.rejectBtn.TabIndex = 26;
            this.rejectBtn.Text = "REJECT";
            this.rejectBtn.UseVisualStyleBackColor = false;
            this.rejectBtn.Click += new System.EventHandler(this.rejectBtn_Click);
            // 
            // refreshBtn
            // 
            this.refreshBtn.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.refreshBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.refreshBtn.Location = new System.Drawing.Point(1372, 405);
            this.refreshBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(188, 71);
            this.refreshBtn.TabIndex = 25;
            this.refreshBtn.Text = "REFRESH";
            this.refreshBtn.UseVisualStyleBackColor = false;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // adminDgv
            // 
            this.adminDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adminDgv.Location = new System.Drawing.Point(45, 326);
            this.adminDgv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.adminDgv.Name = "adminDgv";
            this.adminDgv.RowHeadersWidth = 62;
            this.adminDgv.Size = new System.Drawing.Size(1282, 503);
            this.adminDgv.TabIndex = 24;
            this.adminDgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.adminDgv_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(249, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 60);
            this.label1.TabIndex = 23;
            this.label1.Text = "ADMIN DASHBOARD";
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1612, 576);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.showAllBtn);
            this.Controls.Add(this.cmbFilterStatus);
            this.Controls.Add(this.filterBtn);
            this.Controls.Add(this.IncreaseTotalWithFilter);
            this.Controls.Add(this.approveBtn);
            this.Controls.Add(this.rejectBtn);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.adminDgv);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdminDashboardForm";
            this.Text = "AdminDashboardForm";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adminDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button showAllBtn;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.Label IncreaseTotalWithFilter;
        private System.Windows.Forms.Button approveBtn;
        private System.Windows.Forms.Button rejectBtn;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.DataGridView adminDgv;
        private System.Windows.Forms.Label label1;
    }
}