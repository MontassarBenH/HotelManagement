namespace HotelManagement.Presentation.Forms
{
    partial class AvailableRoomsForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblInfo);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.dtpCheckOut);
            this.panelTop.Controls.Add(this.lblCheckOut);
            this.panelTop.Controls.Add(this.dtpCheckIn);
            this.panelTop.Controls.Add(this.lblCheckIn);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(771, 80);
            this.panelTop.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Location = new System.Drawing.Point(440, 40);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(305, 28);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Bitte Zeitraum wählen und suchen";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(330, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Suchen";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(170, 37);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(150, 33);
            this.dtpCheckOut.TabIndex = 3;
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(170, 8);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(106, 28);
            this.lblCheckOut.TabIndex = 2;
            this.lblCheckOut.Text = "Check-out:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(10, 37);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(150, 33);
            this.dtpCheckIn.TabIndex = 1;
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(10, 11);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(92, 28);
            this.lblCheckIn.TabIndex = 0;
            this.lblCheckIn.Text = "Check-in:";
            // 
            // dgvRooms
            // 
            this.dgvRooms.AllowUserToAddRows = false;
            this.dgvRooms.AllowUserToDeleteRows = false;
            this.dgvRooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRooms.BackgroundColor = System.Drawing.Color.White;
            this.dgvRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRooms.Location = new System.Drawing.Point(0, 80);
            this.dgvRooms.MultiSelect = false;
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.ReadOnly = true;
            this.dgvRooms.RowHeadersWidth = 62;
            this.dgvRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRooms.Size = new System.Drawing.Size(771, 446);
            this.dgvRooms.TabIndex = 1;
            this.dgvRooms.DoubleClick += new System.EventHandler(this.dgvRooms_DoubleClick);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnSelect);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 526);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(771, 50);
            this.panelBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(590, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(124)))), ((int)(((byte)(16)))));
            this.btnSelect.Enabled = false;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(460, 7);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(120, 40);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Zimmer auswählen";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // AvailableRoomsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 576);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "AvailableRoomsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Verfügbare Zimmer suchen";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvRooms;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClose;
    }
}
