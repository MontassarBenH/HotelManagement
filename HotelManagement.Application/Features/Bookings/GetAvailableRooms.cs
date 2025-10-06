using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Features.Bookings
{
    public class GetAvailableRoomsQuery : IRequest<List<Room>>
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

    public class GetAvailableRoomsHandler : IRequestHandler<GetAvailableRoomsQuery, List<Room>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public GetAvailableRoomsHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Room>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
        {
            // Alle Zimmer holen
            var allRooms = await _roomRepository.GetAllRoomsAsync();

            // Nur verfügbare Zimmer (generell)
            var availableRooms = allRooms.Where(r => r.IsAvailable).ToList();

            // Alle Buchungen holen
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            // Filter: Zimmer die im gewünschten Zeitraum NICHT gebucht sind
            var availableInTimeframe = availableRooms.Where(room =>
            {
                var hasOverlap = bookings.Any(b =>
                    b.RoomNumber == room.RoomNumber &&
                    !(request.CheckOutDate <= b.CheckInDate || request.CheckInDate >= b.CheckOutDate)
                );

                return !hasOverlap; 
            }).ToList();

            System.Diagnostics.Debug.WriteLine($"Verfügbare Zimmer im Zeitraum {request.CheckInDate:dd.MM.yyyy} - {request.CheckOutDate:dd.MM.yyyy}: {availableInTimeframe.Count}");

            return availableInTimeframe;
        }
    }
}