using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Features.Rooms
{
    public class CheckAndUpdateRoomStatusesCommand : IRequest<RoomStatusUpdateResult>
    {
    }

    public class RoomStatusUpdateResult
    {
        public int RoomsChecked { get; set; }
        public int RoomsMarkedUnavailable { get; set; }
        public int RoomsMarkedAvailable { get; set; }
        public string Message { get; set; }
    }

    public class CheckAndUpdateRoomStatusesHandler : IRequestHandler<CheckAndUpdateRoomStatusesCommand, RoomStatusUpdateResult>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public CheckAndUpdateRoomStatusesHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<RoomStatusUpdateResult> Handle(CheckAndUpdateRoomStatusesCommand request, CancellationToken cancellationToken)
        {
            var result = new RoomStatusUpdateResult();

            try
            {
                System.Diagnostics.Debug.WriteLine("=== STARTE ZIMMER-STATUS UPDATE ===");

                var today = DateTime.Today;

                // Alle Zimmer holen
                var allRooms = await _roomRepository.GetAllRoomsAsync();
                result.RoomsChecked = allRooms.Count;

                // Alle Buchungen holen
                var allBookings = await _bookingRepository.GetAllBookingsAsync();

                foreach (var room in allRooms)
                {
                    bool isOccupiedToday = allBookings.Any(b =>
                        b.RoomNumber == room.RoomNumber &&
                        b.CheckInDate <= today &&
                        b.CheckOutDate > today);

                    // write only BelegtHeute, never Verfügbar
                    await _roomRepository.SetOccupiedTodayAsync(room.RoomNumber, isOccupiedToday);

                    if (isOccupiedToday)
                        result.RoomsMarkedUnavailable++;
                    else
                        result.RoomsMarkedAvailable++;
                }

                result.Message = $"Status-Update abgeschlossen: {result.RoomsMarkedUnavailable} belegt, {result.RoomsMarkedAvailable} frei";
                System.Diagnostics.Debug.WriteLine($"=== ZIMMER-STATUS UPDATE FERTIG: {result.Message} ===");
            }
            catch (Exception ex)
            {
                result.Message = $"Fehler: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Fehler beim Status-Update: {ex.Message}");
            }

            return result;
        }
    }
}
