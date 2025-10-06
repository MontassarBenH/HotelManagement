using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HotelManagement.Application;
using HotelManagement.Application.Models;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Infrastructure.Repositories
{
    public class XmlBookingRepository : IBookingRepository
    {
        private readonly string _storageFolder;

        public XmlBookingRepository(string storageFolder)
        {
            _storageFolder = storageFolder;
            if (!Directory.Exists(_storageFolder))
            {
                Directory.CreateDirectory(_storageFolder);
            }
        }

        public Task SaveBookingAsync(Booking booking)
        {
            return Task.Run(() =>
            {
                var filePath = Path.Combine(_storageFolder, $"{booking.BookingId}.xml");
                var serializer = new XmlSerializer(typeof(Booking));

                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, booking);
                }
            });
        }

        public Task<List<Booking>> GetAllBookingsAsync()
        {
            return Task.Run(() =>
            {
                var bookings = new List<Booking>();
                var files = Directory.GetFiles(_storageFolder, "*.xml");
                var serializer = new XmlSerializer(typeof(Booking));

                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        var booking = (Booking)serializer.Deserialize(reader);
                        bookings.Add(booking);
                    }
                }

                return bookings;
            });
        }

        public Task<Booking> GetBookingByIdAsync(string bookingId)
        {
            return Task.Run(() =>
            {
                var filePath = Path.Combine(_storageFolder, $"{bookingId}.xml");
                if (!File.Exists(filePath))
                    return null;

                var serializer = new XmlSerializer(typeof(Booking));
                using (var reader = new StreamReader(filePath))
                {
                    return (Booking)serializer.Deserialize(reader);
                }
            });
        }
    }
}