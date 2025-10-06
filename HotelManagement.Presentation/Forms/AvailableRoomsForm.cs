using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MediatR;
using HotelManagement.Application.Features.Bookings;
using HotelManagement.Infrastructure.Repositories;

namespace HotelManagement.Presentation.Forms
{
    public partial class AvailableRoomsForm : Form
    {
        public string SelectedRoomNumber { get; private set; }

        public AvailableRoomsForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Initial-Werte setzen
            dtpCheckIn.MinDate = DateTime.Today;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.MinDate = DateTime.Today.AddDays(1);
            dtpCheckOut.Value = DateTime.Today.AddDays(1);

            // Event Handler
            dgvRooms.SelectionChanged += DgvRooms_SelectionChanged;
        }

        private void DgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = dgvRooms.SelectedRows.Count > 0;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                btnSearch.Text = "⏳ Suche...";
                lblInfo.Text = "Suche läuft...";
                lblInfo.ForeColor = Color.Gray;

                var mediator = Globals.ThisAddIn.ServiceProvider.GetService(typeof(IMediator)) as IMediator;
                var roomRepo = new ExcelRoomRepository(Globals.ThisAddIn.Application);

                // Daten laden
                var allRooms = await roomRepo.GetAllRoomsAsync();
                var allBookings = await mediator.Send(new GetAllBookingsQuery());

                // Filter verfügbare Zimmer für den Zeitraum
                var checkIn = dtpCheckIn.Value.Date;
                var checkOut = dtpCheckOut.Value.Date;

                var availableRooms = allRooms.Where(room =>
                {
                    // Zimmer muss generell verfügbar sein
                    if (!room.IsAvailable) return false;

                    // Prüfe Überschneidungen mit Buchungen
                    var hasConflict = allBookings.Any(booking =>
                        booking.RoomNumber == room.RoomNumber &&
                        !(checkOut <= booking.CheckInDate || checkIn >= booking.CheckOutDate)
                    );

                    return !hasConflict;
                }).ToList();

                // Anzeigen
                var nights = (checkOut - checkIn).Days;
                var displayData = availableRooms.Select(r => new
                {
                    Zimmernummer = r.RoomNumber,
                    Typ = r.RoomType,
                    Status = "✓ Verfügbar",
                    Nächte = nights,
                    Preis = (nights * 100m).ToString("C")
                }).ToList();

                dgvRooms.DataSource = displayData;

                // Spaltenbreiten
                if (dgvRooms.Columns.Count > 0)
                {
                    dgvRooms.Columns["Zimmernummer"].Width = 120;
                    dgvRooms.Columns["Typ"].Width = 150;
                    dgvRooms.Columns["Status"].Width = 120;
                    dgvRooms.Columns["Nächte"].Width = 80;
                    dgvRooms.Columns["Preis"].Width = 100;
                }

                // Status
                lblInfo.Text = $"✓ {availableRooms.Count} Zimmer verfügbar";
                lblInfo.ForeColor = Color.Green;

                if (availableRooms.Count == 0)
                {
                    lblInfo.Text = "❌ Keine Zimmer im gewählten Zeitraum verfügbar";
                    lblInfo.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden:\n\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblInfo.Text = "❌ Fehler bei der Suche";
                lblInfo.ForeColor = Color.Red;
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "Suchen";
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                SelectedRoomNumber = dgvRooms.SelectedRows[0].Cells["Zimmernummer"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvRooms_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
