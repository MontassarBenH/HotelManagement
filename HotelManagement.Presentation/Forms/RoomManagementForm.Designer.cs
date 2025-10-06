namespace HotelManagement.Presentation.Forms
{
    partial class RoomManagementForm
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
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.groupBoxEdit = new System.Windows.Forms.GroupBox();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.cboRoomType = new System.Windows.Forms.ComboBox();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.chkAvailable = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.groupBoxEdit.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 50);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Zimmerverwaltung";
            // 
            // dgvRooms
            // 
            this.dgvRooms.AllowUserToAddRows = false;
            this.dgvRooms.AllowUserToDeleteRows = false;
            this.dgvRooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRooms.Location = new System.Drawing.Point(0, 50);
            this.dgvRooms.MultiSelect = false;
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.ReadOnly = true;
            this.dgvRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRooms.Size = new System.Drawing.Size(900, 300);
            this.dgvRooms.TabIndex = 1;
            this.dgvRooms.SelectionChanged += new System.EventHandler(this.dgvRooms_SelectionChanged);
            // 
            // groupBoxEdit
            // 
            this.groupBoxEdit.Controls.Add(this.btnClear);
            this.groupBoxEdit.Controls.Add(this.btnDelete);
            this.groupBoxEdit.Controls.Add(this.btnUpdate);
            this.groupBoxEdit.Controls.Add(this.btnAdd);
            this.groupBoxEdit.Controls.Add(this.chkAvailable);
            this.groupBoxEdit.Controls.Add(this.lblAvailable);
            this.groupBoxEdit.Controls.Add(this.cboRoomType);
            this.groupBoxEdit.Controls.Add(this.lblRoomType);
            this.groupBoxEdit.Controls.Add(this.txtRoomNumber);
            this.groupBoxEdit.Controls.Add(this.lblRoomNumber);
            this.groupBoxEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxEdit.Location = new System.Drawing.Point(0, 350);
            this.groupBoxEdit.Name = "groupBoxEdit";
            this.groupBoxEdit.Size = new System.Drawing.Size(900, 150);
            this.groupBoxEdit.TabIndex = 2;
            this.groupBoxEdit.TabStop = false;
            this.groupBoxEdit.Text = "Zimmer bearbeiten";
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Location = new System.Drawing.Point(20, 35);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(85, 13);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "Zimmernummer:";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(130, 32);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(150, 20);
            this.txtRoomNumber.TabIndex = 1;
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.Location = new System.Drawing.Point(20, 70);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(60, 13);
            this.lblRoomType.TabIndex = 2;
            this.lblRoomType.Text = "Zimmertyp:";
            // 
            // cboRoomType
            // 
            this.cboRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoomType.FormattingEnabled = true;
            this.cboRoomType.Items.AddRange(new object[] {
            "Einzel",
            "Doppel",
            "Suite",
            "Deluxe",
            "Präsidenten-Suite"});
            this.cboRoomType.Location = new System.Drawing.Point(130, 67);
            this.cboRoomType.Name = "cboRoomType";
            this.cboRoomType.Size = new System.Drawing.Size(200, 21);
            this.cboRoomType.TabIndex = 3;
            // 
            // lblAvailable
            // 
            this.lblAvailable.AutoSize = true;
            this.lblAvailable.Location = new System.Drawing.Point(20, 105);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(60, 13);
            this.lblAvailable.TabIndex = 4;
            this.lblAvailable.Text = "Verfügbar:";
            // 
            // chkAvailable
            // 
            this.chkAvailable.AutoSize = true;
            this.chkAvailable.Checked = true;
            this.chkAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvailable.Location = new System.Drawing.Point(130, 104);
            this.chkAvailable.Name = "chkAvailable";
            this.chkAvailable.Size = new System.Drawing.Size(15, 14);
            this.chkAvailable.TabIndex = 5;
            this.chkAvailable.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(400, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Hinzufügen";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(510, 30);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Aktualisieren";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(620, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(730, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Leeren";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblCount);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 500);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(900, 50);
            this.panelBottom.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(20, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(50, 13);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "0 Zimmer";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(670, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Aktualisieren";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(780, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Schließen";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // RoomManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.groupBoxEdit);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "RoomManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zimmerverwaltung";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.groupBoxEdit.ResumeLayout(false);
            this.groupBoxEdit.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvRooms;
        private System.Windows.Forms.GroupBox groupBoxEdit;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.ComboBox cboRoomType;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.CheckBox chkAvailable;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblCount;
    }
}
