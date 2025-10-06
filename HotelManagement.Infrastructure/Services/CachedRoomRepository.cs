using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Services
{
    public class CachedRoomRepository : IRoomRepository
    {
        private readonly IRoomRepository _innerRepository;
        private List<Room> _cachedRooms;
        private DateTime _cacheExpiration;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public CachedRoomRepository(IRoomRepository innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            if (_cachedRooms == null || DateTime.Now > _cacheExpiration)
            {
                _cachedRooms = await _innerRepository.GetAllRoomsAsync();
                _cacheExpiration = DateTime.Now.Add(_cacheDuration);
                System.Diagnostics.Debug.WriteLine("Cache refreshed");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Returning cached rooms");
            }

            return new List<Room>(_cachedRooms); 
        }

        public async Task<Room> GetRoomByNumberAsync(string roomNumber)
        {
            var rooms = await GetAllRoomsAsync();
            return rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public async Task SetOccupiedTodayAsync(string roomNumber, bool isOccupiedToday)
        {
            await _innerRepository.SetOccupiedTodayAsync(roomNumber, isOccupiedToday);
         
        }


        public async Task AddRoomAsync(Room room)
        {
            await _innerRepository.AddRoomAsync(room);
            InvalidateCache();
        }

        public async Task UpdateRoomAsync(Room room)
        {
            await _innerRepository.UpdateRoomAsync(room);
            InvalidateCache();
        }

        public async Task DeleteRoomAsync(string roomNumber)
        {
            await _innerRepository.DeleteRoomAsync(roomNumber);
            InvalidateCache();
        }

        private void InvalidateCache()
        {
            _cachedRooms = null;
            System.Diagnostics.Debug.WriteLine("Cache invalidated");
        }
    }
}