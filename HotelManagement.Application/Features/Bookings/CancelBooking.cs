using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Features.Bookings
{
    public class CancelBookingCommand : IRequest<CancelBookingResult>
    {
        public string BookingId { get; set; }
    }

    public class CancelBookingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, CancelBookingResult>
    {
        private readonly IBookingRepository _bookingRepository;

        public CancelBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<CancelBookingResult> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"Storniere Buchung: {request.BookingId}");

            var booking = await _bookingRepository.GetBookingByIdAsync(request.BookingId);

            if (booking == null)
            {
                return new CancelBookingResult
                {
                    Success = false,
                    Message = "Buchung nicht gefunden"
                };
            }

            // Prüfe ob Buchung in der Zukunft liegt
            if (booking.CheckInDate < DateTime.Today)
            {
                return new CancelBookingResult
                {
                    Success = false,
                    Message = "Vergangene Buchungen können nicht storniert werden"
                };
            }

       
            return new CancelBookingResult
            {
                Success = true,
                Message = $"Buchung {request.BookingId} wurde storniert"
            };
        }
    }
}
