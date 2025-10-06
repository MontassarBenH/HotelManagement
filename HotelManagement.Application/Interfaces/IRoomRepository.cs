using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByNumberAsync(string roomNumber);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(string roomNumber);

        Task SetOccupiedTodayAsync(string roomNumber, bool isOccupiedToday);

    }
}