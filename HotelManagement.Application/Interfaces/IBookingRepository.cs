using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task SaveBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(string bookingId);
    }
}