using Microsoft.Office.Tools.Ribbon;

namespace HotelManagement.Presentation
{
    partial class HotelRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public HotelRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.groupHotel = this.Factory.CreateRibbonGroup();
            this.btnNewBooking = this.Factory.CreateRibbonButton();
            this.btnViewBookings = this.Factory.CreateRibbonButton();
            this.btnManageRooms = this.Factory.CreateRibbonButton();
            this.btnAvailableRooms = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnUpdateStatus = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.groupHotel.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.groupHotel);
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "Hotel Management";
            this.tab1.Name = "tab1";
            // 
            // groupHotel
            // 
            this.groupHotel.Items.Add(this.btnNewBooking);
            this.groupHotel.Items.Add(this.btnViewBookings);
            this.groupHotel.Items.Add(this.btnManageRooms);
            this.groupHotel.Items.Add(this.btnAvailableRooms);
            this.groupHotel.Label = "Buchungen";
            this.groupHotel.Name = "groupHotel";
            // 
            // btnNewBooking
            // 
            this.btnNewBooking.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnNewBooking.Image = global::HotelManagement.Presentation.Properties.Resources.add_event_6756413;
            this.btnNewBooking.Label = "Neue Buchung";
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.ShowImage = true;
            this.btnNewBooking.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNewBooking_Click);
            // 
            // btnViewBookings
            // 
            this.btnViewBookings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnViewBookings.Image = global::HotelManagement.Presentation.Properties.Resources.schedule_13005751;
            this.btnViewBookings.Label = "Buchungen anzeigen";
            this.btnViewBookings.Name = "btnViewBookings";
            this.btnViewBookings.ShowImage = true;
            this.btnViewBookings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnViewBookings_Click);
            // 
            // btnManageRooms
            // 
            this.btnManageRooms.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnManageRooms.Image = global::HotelManagement.Presentation.Properties.Resources.sleep_bed_18005867;
            this.btnManageRooms.Label = "Zimmer verwalten";
            this.btnManageRooms.Name = "btnManageRooms";
            this.btnManageRooms.ShowImage = true;
            this.btnManageRooms.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnManageRooms_Click);
            // 
            // btnAvailableRooms
            // 
            this.btnAvailableRooms.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAvailableRooms.Image = global::HotelManagement.Presentation.Properties.Resources.room_available_18671757;
            this.btnAvailableRooms.Label = "Verfügbare Zimmer";
            this.btnAvailableRooms.Name = "btnAvailableRooms";
            this.btnAvailableRooms.ShowImage = true;
            this.btnAvailableRooms.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAvailableRooms_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnUpdateStatus);
            this.group1.Label = "Verwaltung";
            this.group1.Name = "group1";
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Image = global::HotelManagement.Presentation.Properties.Resources.rest_7406704;
            this.btnUpdateStatus.Label = "Status Update";
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.ShowImage = true;
            // 
            // HotelRibbon
            // 
            this.Name = "HotelRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.HotelRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.groupHotel.ResumeLayout(false);
            this.groupHotel.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupHotel;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNewBooking;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnViewBookings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnManageRooms;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAvailableRooms;
        internal RibbonGroup group1;
        internal RibbonButton btnUpdateStatus;
    }

    partial class ThisRibbonCollection
    {
        internal HotelRibbon HotelRibbon
        {
            get { return this.GetRibbon<HotelRibbon>(); }
        }
    }
}

