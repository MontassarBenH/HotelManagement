using MediatR;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Windows.Forms;

namespace HotelManagement.Presentation
{
    public partial class HotelRibbon
    {
        private void HotelRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Ribbon geladen
        }

        private void btnNewBooking_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var form = new Forms.BookingForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Öffnen des Formulars:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdateStatus_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var mediator = Globals.ThisAddIn.ServiceProvider.GetService(typeof(IMediator)) as IMediator;

                var command = new HotelManagement.Application.Features.Rooms.CheckAndUpdateRoomStatusesCommand();
                var result = await mediator.Send(command);

                MessageBox.Show(
                    $"Zimmer-Status Update durchgeführt!\n\n" +
                    $"Geprüfte Zimmer: {result.RoomsChecked}\n" +
                    $"Als belegt markiert: {result.RoomsMarkedUnavailable}\n" +
                    $"Als frei markiert: {result.RoomsMarkedAvailable}",
                    "Status Update",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}", "Fehler");
            }
        }



        private void btnAvailableRooms_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                using (var form = new Forms.AvailableRoomsForm())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Öffnen der verfügbaren Zimmer:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnViewBookings_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var form = new Forms.BookingListForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Öffnen der Buchungsliste:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnManageRooms_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var form = new Forms.RoomManagementForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Öffnen der Zimmerverwaltung:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
