using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Models
{
    public partial class Booking
    {
        public string BookingId { get; set; }
        public Guest Guest { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public bool IsActive =>
            CheckInDate <= DateTime.Today && CheckOutDate > DateTime.Today;

        public bool StartsToday =>
            CheckInDate.Date == DateTime.Today;

        public bool EndsToday =>
            CheckOutDate.Date == DateTime.Today;
        public decimal DailyRate { get; set; } = 100.00m;

        public int NumberOfNights => (CheckOutDate - CheckInDate).Days;
        public decimal TotalPrice => NumberOfNights * DailyRate;

        public bool IsValid()
        {
            return CheckInDate < CheckOutDate &&
                   !string.IsNullOrEmpty(BookingId) &&
                   !string.IsNullOrEmpty(RoomNumber) &&
                   Guest != null;
        }

        public BookingStatus GetStatus()
        {
            var today = DateTime.Today;

            if (CheckOutDate < today)
                return BookingStatus.Completed;

            if (CheckInDate > today)
                return BookingStatus.Upcoming;

            if (CheckInDate <= today && CheckOutDate > today)
                return BookingStatus.Active;

            return BookingStatus.Unknown;
        }
        public enum BookingStatus
        {
            Upcoming,   
            Active,     
            Completed, 
            Unknown
        }
    }
}
