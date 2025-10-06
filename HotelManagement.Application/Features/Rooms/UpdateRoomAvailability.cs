using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Features.Rooms
{
    public class UpdateRoomAvailabilityCommand : IRequest<bool>
    {
        public string RoomNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string Reason { get; set; } 
    }

    public class UpdateRoomAvailabilityHandler : IRequestHandler<UpdateRoomAvailabilityCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;

        public UpdateRoomAvailabilityHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<bool> Handle(UpdateRoomAvailabilityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Update Zimmer {request.RoomNumber} Verfügbarkeit: {(request.IsAvailable ? "Ja" : "Nein")} - Grund: {request.Reason}");

                var room = await _roomRepository.GetRoomByNumberAsync(request.RoomNumber);

                if (room == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Zimmer {request.RoomNumber} nicht gefunden!");
                    return false;
                }

                room.IsAvailable = request.IsAvailable;
                await _roomRepository.UpdateRoomAsync(room);

                System.Diagnostics.Debug.WriteLine($"✓ Zimmer {request.RoomNumber} Status aktualisiert");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Status-Update: {ex.Message}");
                return false;
            }
        }
    }
}
