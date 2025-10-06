namespace HotelManagement.Presentation.Forms
{
    partial class BookingListForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 23);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Alle Buchungen";
            // 
            // dgvBookings
            // 
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookings.Location = new System.Drawing.Point(0, 77);
            this.dgvBookings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.RowHeadersWidth = 62;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.Size = new System.Drawing.Size(1200, 538);
            this.dgvBookings.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(855, 15);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 46);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Aktualisieren";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1020, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 46);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 77);
            this.panelTop.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblCount);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 615);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 77);
            this.panelBottom.TabIndex = 2;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(30, 28);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(104, 20);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0 Buchungen";
            // 
            // BookingListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.dgvBookings);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BookingListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buchungsübersicht";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblCount;
    }
}

