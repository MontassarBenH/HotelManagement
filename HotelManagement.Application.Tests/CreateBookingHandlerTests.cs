using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Features.Bookings
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, BookingResult>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateBookingHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<BookingResult> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== CREATE BOOKING HANDLER START ===");
                System.Diagnostics.Debug.WriteLine($"Gast: {request.GuestName}");
                System.Diagnostics.Debug.WriteLine($"Zimmer: {request.RoomNumber}");

                // Validierung: Datum (zusätzlich zu FluentValidation)
                if (request.CheckInDate >= request.CheckOutDate)
                {
                    return new BookingResult
                    {
                        Success = false,
                        Message = "Check-in Datum muss vor Check-out Datum liegen"
                    };
                }

             
                var room = await _roomRepository.GetRoomByNumberAsync(request.RoomNumber);

                if (room == null)
                {
                    return new BookingResult
                    {
                        Success = false,
                        Message = $"Zimmer '{request.RoomNumber}' existiert nicht."
                    };
                }

                if (!room.IsAvailable)
                {
                    return new BookingResult
                    {
                        Success = false,
                        Message = $"Zimmer '{request.RoomNumber}' ist derzeit außer Betrieb"
                    };
                }

                // Überschneidungen prüfen
                var existingBookings = await _bookingRepository.GetAllBookingsAsync();

                var overlappingBooking = existingBookings.FirstOrDefault(b =>
                    b.RoomNumber == request.RoomNumber &&
                    !(request.CheckOutDate <= b.CheckInDate || request.CheckInDate >= b.CheckOutDate)
                );

                if (overlappingBooking != null)
                {
                    return new BookingResult
                    {
                        Success = false,
                        Message = $"Zimmer '{request.RoomNumber}' ist bereits gebucht vom {overlappingBooking.CheckInDate:dd.MM.yyyy} bis {overlappingBooking.CheckOutDate:dd.MM.yyyy}\n\n" +
                                 $"Gebuchter Gast: {overlappingBooking.Guest?.Name}"
                    };
                }

                // Buchung erstellen
                var booking = new Booking
                {
                    BookingId = Guid.NewGuid().ToString(),
                    Guest = new Guest
                    {
                        Id = request.GuestId,
                        Name = request.GuestName
                    },
                    RoomNumber = request.RoomNumber,
                    CheckInDate = request.CheckInDate.Date,
                    CheckOutDate = request.CheckOutDate.Date
                };

                if (!booking.IsValid())
                {
                    return new BookingResult
                    {
                        Success = false,
                        Message = "Buchungsdaten sind ungültig"
                    };
                }

                // Speichern
                await _bookingRepository.SaveBookingAsync(booking);
                System.Diagnostics.Debug.WriteLine("=== BUCHUNG ERFOLGREICH ===");

                return new BookingResult
                {
                    Success = true,
                    BookingId = booking.BookingId,
                    Message = $"Buchung erfolgreich erstellt!",
                    Booking = booking
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FEHLER: {ex.Message}");
                return new BookingResult
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen der Buchung: {ex.Message}"
                };
            }
        }
    }
}