using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.Bookings
{
    public class CreateBookingCommand : IRequest<BookingResult>
    {
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

    public class BookingResult
    {
        public bool Success { get; set; }
        public string BookingId { get; set; }
        public string Message { get; set; }
        public Booking Booking { get; set; }
    }

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
            System.Diagnostics.Debug.WriteLine("=== CREATE BOOKING HANDLER START ===");
            System.Diagnostics.Debug.WriteLine($"Gast: {request.GuestName}");
            System.Diagnostics.Debug.WriteLine($"Zimmer: {request.RoomNumber}");
            System.Diagnostics.Debug.WriteLine($"Check-in: {request.CheckInDate:dd.MM.yyyy}");
            System.Diagnostics.Debug.WriteLine($"Check-out: {request.CheckOutDate:dd.MM.yyyy}");

            // Validierung 1: Datum
            if (request.CheckInDate >= request.CheckOutDate)
            {
                System.Diagnostics.Debug.WriteLine("FEHLER: Check-in >= Check-out");
                return new BookingResult
                {
                    Success = false,
                    Message = "Check-in Datum muss vor Check-out Datum liegen"
                };
            }

            // Validierung 2: Zimmer existiert
            System.Diagnostics.Debug.WriteLine($"Prüfe Zimmer-Existenz: '{request.RoomNumber}'");
            var room = await _roomRepository.GetRoomByNumberAsync(request.RoomNumber);

            if (room == null)
            {
                System.Diagnostics.Debug.WriteLine("FEHLER: Zimmer nicht gefunden");
                return new BookingResult
                {
                    Success = false,
                    Message = $"Zimmer '{request.RoomNumber}' existiert nicht. Bitte prüfen Sie die Zimmernummer."
                };
            }

            if (!room.IsAvailable)
            {
                System.Diagnostics.Debug.WriteLine("FEHLER: Zimmer generell nicht verfügbar");
                return new BookingResult
                {
                    Success = false,
                    Message = $"Zimmer '{request.RoomNumber}' ist derzeit außer Betrieb"
                };
            }

            // Validierung 3: Zimmer ist im Zeitraum verfügbar
            System.Diagnostics.Debug.WriteLine("Prüfe Überschneidungen mit existierenden Buchungen...");
            var existingBookings = await _bookingRepository.GetAllBookingsAsync();

            var overlappingBooking = existingBookings.FirstOrDefault(b =>
                b.RoomNumber == request.RoomNumber &&
                !(request.CheckOutDate <= b.CheckInDate || request.CheckInDate >= b.CheckOutDate)
            );

            if (overlappingBooking != null)
            {
                System.Diagnostics.Debug.WriteLine($"FEHLER: Überschneidung gefunden mit Buchung {overlappingBooking.BookingId}");
                return new BookingResult
                {
                    Success = false,
                    Message = $"Zimmer '{request.RoomNumber}' ist bereits gebucht vom {overlappingBooking.CheckInDate:dd.MM.yyyy} bis {overlappingBooking.CheckOutDate:dd.MM.yyyy}\n\n" +
                             $"Gebuchter Gast: {overlappingBooking.Guest?.Name}\n" +
                             $"Buchungs-ID: {overlappingBooking.BookingId}"
                };
            }

            System.Diagnostics.Debug.WriteLine("✓ Keine Überschneidungen - Zimmer verfügbar!");

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
                System.Diagnostics.Debug.WriteLine("FEHLER: Buchung nicht valid");
                return new BookingResult
                {
                    Success = false,
                    Message = "Buchungsdaten sind ungültig"
                };
            }

            // Speichern
            try
            {
                System.Diagnostics.Debug.WriteLine($"Speichere Buchung: {booking.BookingId}");
                await _bookingRepository.SaveBookingAsync(booking);
                System.Diagnostics.Debug.WriteLine("✓ Buchung erfolgreich gespeichert!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FEHLER beim Speichern: {ex.Message}");
                return new BookingResult
                {
                    Success = false,
                    Message = $"Fehler beim Speichern: {ex.Message}"
                };
            } 

           /* if (booking.CheckInDate.Date == DateTime.Today)
            {
                System.Diagnostics.Debug.WriteLine($"Buchung beginnt HEUTE - Setze Zimmer {booking.RoomNumber} auf 'Nicht verfügbar'");

                room.IsAvailable = false;
                await _roomRepository.UpdateRoomAsync(room);

                System.Diagnostics.Debug.WriteLine($"✓ Zimmer {booking.RoomNumber} Status aktualisiert");
            }*/

            System.Diagnostics.Debug.WriteLine("=== CREATE BOOKING HANDLER SUCCESS ===");

            return new BookingResult
            {
                Success = true,
                BookingId = booking.BookingId,
                Message = $"Buchung erfolgreich erstellt!\n\n" +
                         $"Gast: {booking.Guest.Name}\n" +
                         $"Zimmer: {booking.RoomNumber} ({room.RoomType})\n" +
                         $"Zeitraum: {booking.CheckInDate:dd.MM.yyyy} - {booking.CheckOutDate:dd.MM.yyyy}\n" +
                         $"Nächte: {booking.NumberOfNights}\n" +
                         $"Gesamtpreis: {booking.TotalPrice:C}",
                Booking = booking
            };
        }
    }
}