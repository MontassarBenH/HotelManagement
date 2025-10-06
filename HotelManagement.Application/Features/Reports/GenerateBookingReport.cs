
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Features.Reports
{
    public class GenerateBookingReportQuery : IRequest<BookingReportResult>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RoomNumber { get; set; } 
    }

    public class BookingReportResult
    {
        public int TotalBookings { get; set; }
        public int TotalNights { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> BookingsPerRoom { get; set; }
        public List<BookingReportItem> Items { get; set; }
    }

    public class BookingReportItem
    {
        public string BookingId { get; set; }
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Nights { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class GenerateBookingReportHandler : IRequestHandler<GenerateBookingReportQuery, BookingReportResult>
    {
        private readonly IBookingRepository _bookingRepository;

        public GenerateBookingReportHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingReportResult> Handle(GenerateBookingReportQuery request, CancellationToken cancellationToken)
        {
            var allBookings = await _bookingRepository.GetAllBookingsAsync();

            // Filter by date range
            var filteredBookings = allBookings.Where(b =>
                b.CheckInDate >= request.StartDate &&
                b.CheckOutDate <= request.EndDate).ToList();

            // Optional: Filter by room
            if (!string.IsNullOrEmpty(request.RoomNumber))
            {
                filteredBookings = filteredBookings
                    .Where(b => b.RoomNumber == request.RoomNumber)
                    .ToList();
            }

            var report = new BookingReportResult
            {
                TotalBookings = filteredBookings.Count,
                TotalNights = filteredBookings.Sum(b => b.NumberOfNights),
                TotalRevenue = filteredBookings.Sum(b => b.TotalPrice),
                BookingsPerRoom = filteredBookings
                    .GroupBy(b => b.RoomNumber)
                    .ToDictionary(g => g.Key, g => g.Count()),
                Items = filteredBookings.Select(b => new BookingReportItem
                {
                    BookingId = b.BookingId,
                    GuestName = b.Guest?.Name,
                    RoomNumber = b.RoomNumber,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    Nights = b.NumberOfNights,
                    TotalPrice = b.TotalPrice
                }).OrderBy(i => i.CheckIn).ToList()
            };

            return report;
        }
    }
}