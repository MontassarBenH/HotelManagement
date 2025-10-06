using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Interfaces
{
    public interface IGuestRepository
    {
        Task<List<Guest>> GetAllGuestsAsync();
        Task<Guest> GetGuestByIdAsync(string guestId);
    }
}