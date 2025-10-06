namespace HotelManagement.Presentation.Forms
{
    partial class BookingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (errorProvider != null)
                {
                    errorProvider.Dispose(); 
                }
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGuestName = new System.Windows.Forms.Label();
            this.txtGuestName = new System.Windows.Forms.TextBox();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();


            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Neue Buchung erstellen";
            // 
            // lblGuestName
            // 
            this.lblGuestName.AutoSize = true;
            this.lblGuestName.Location = new System.Drawing.Point(20, 70);
            this.lblGuestName.Name = "lblGuestName";
            this.lblGuestName.Size = new System.Drawing.Size(70, 13);
            this.lblGuestName.TabIndex = 1;
            this.lblGuestName.Text = "Gast-Name:";
            // 
            // txtGuestName
            // 
            this.txtGuestName.Location = new System.Drawing.Point(150, 67);
            this.txtGuestName.Name = "txtGuestName";
            this.txtGuestName.Size = new System.Drawing.Size(300, 20);
            this.txtGuestName.TabIndex = 2;
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Location = new System.Drawing.Point(20, 110);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(85, 13);
            this.lblRoomNumber.TabIndex = 3;
            this.lblRoomNumber.Text = "Zimmernummer:";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(150, 107);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(150, 20);
            this.txtRoomNumber.TabIndex = 4;
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(20, 150);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(100, 13);
            this.lblCheckIn.TabIndex = 5;
            this.lblCheckIn.Text = "Check-in Datum:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(150, 147);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckIn.TabIndex = 6;
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(20, 190);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(110, 13);
            this.lblCheckOut.TabIndex = 7;
            this.lblCheckOut.Text = "Check-out Datum:";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(150, 187);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckOut.TabIndex = 8;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 240);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(484, 50);
            this.panelButtons.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(250, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Buchen";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(360, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 290);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.lblCheckOut);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.lblCheckIn);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(this.lblRoomNumber);
            this.Controls.Add(this.txtGuestName);
            this.Controls.Add(this.lblGuestName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Buchung";
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGuestName;
        private System.Windows.Forms.TextBox txtGuestName;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelButtons;
    }
}
