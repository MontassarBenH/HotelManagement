using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Models;
using HotelManagement.Infrastructure.Repositories;

namespace HotelManagement.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class XmlBookingRepositoryTests
    {
        private string _testFolder;
        private XmlBookingRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _testFolder = Path.Combine(Path.GetTempPath(), "HotelTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_testFolder);
            _repository = new XmlBookingRepository(_testFolder);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_testFolder))
            {
                Directory.Delete(_testFolder, true);
            }
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task SaveBookingAsync_CreatesXmlFile()
        {
            // Arrange
            var booking = CreateTestBooking("B001", "101");

            // Act
            await _repository.SaveBookingAsync(booking);

            // Assert
            var filePath = Path.Combine(_testFolder, "B001.xml");
            File.Exists(filePath).Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task SaveBookingAsync_XmlContainsCorrectData()
        {
            // Arrange
            var booking = CreateTestBooking("B002", "102");
            booking.Guest.Name = "Test User 123";

            // Act
            await _repository.SaveBookingAsync(booking);

            // Assert
            var filePath = Path.Combine(_testFolder, "B002.xml");
            var xmlContent = File.ReadAllText(filePath);

            xmlContent.Should().Contain("B002");
            xmlContent.Should().Contain("102");
            xmlContent.Should().Contain("Test User 123");
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task GetBookingByIdAsync_ReturnsCorrectBooking()
        {
            // Arrange
            var booking = CreateTestBooking("B003", "103");
            await _repository.SaveBookingAsync(booking);

            // Act
            var retrieved = await _repository.GetBookingByIdAsync("B003");

            // Assert
            retrieved.Should().NotBeNull();
            retrieved.BookingId.Should().Be("B003");
            retrieved.RoomNumber.Should().Be("103");
            retrieved.Guest.Name.Should().Be("Max Mustermann");
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task GetBookingByIdAsync_NonExistent_ReturnsNull()
        {
            // Act
            var retrieved = await _repository.GetBookingByIdAsync("NOTEXIST");

            // Assert
            retrieved.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task GetAllBookingsAsync_ReturnsAllBookings()
        {
            // Arrange
            await _repository.SaveBookingAsync(CreateTestBooking("B004", "101"));
            await _repository.SaveBookingAsync(CreateTestBooking("B005", "102"));
            await _repository.SaveBookingAsync(CreateTestBooking("B006", "103"));

            // Act
            var bookings = await _repository.GetAllBookingsAsync();

            // Assert
            bookings.Should().HaveCount(3);
            bookings.Should().Contain(b => b.BookingId == "B004");
            bookings.Should().Contain(b => b.BookingId == "B005");
            bookings.Should().Contain(b => b.BookingId == "B006");
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task GetAllBookingsAsync_EmptyFolder_ReturnsEmptyList()
        {
            // Act
            var bookings = await _repository.GetAllBookingsAsync();

            // Assert
            bookings.Should().BeEmpty();
        }

        [TestMethod]
        [TestCategory("Persistence")]
        public async Task SaveBookingAsync_OverwritesExistingFile()
        {
            // Arrange
            var booking1 = CreateTestBooking("B007", "101");
            booking1.Guest.Name = "Original Name";
            await _repository.SaveBookingAsync(booking1);

            var booking2 = CreateTestBooking("B007", "102"); 
            booking2.Guest.Name = "Updated Name";

            // Act
            await _repository.SaveBookingAsync(booking2);

            // Assert
            var retrieved = await _repository.GetBookingByIdAsync("B007");
            retrieved.Guest.Name.Should().Be("Updated Name");
            retrieved.RoomNumber.Should().Be("102");
        }

        [TestMethod]
        [TestCategory("Performance")]
        public async Task GetAllBookingsAsync_LargeDataset_PerformsWell()
        {
           
            for (int i = 0; i < 100; i++)
            {
                await _repository.SaveBookingAsync(CreateTestBooking($"B{i:000}", "101"));
            }

            // Act
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var bookings = await _repository.GetAllBookingsAsync();
            stopwatch.Stop();

            // Assert
            bookings.Should().HaveCount(100);
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(1000); // < 1 Sekunde
        }

        private Booking CreateTestBooking(string bookingId, string roomNumber)
        {
            return new Booking
            {
                BookingId = bookingId,
                Guest = new Guest
                {
                    Id = "G001",
                    Name = "Max Mustermann",
                    Email = "max@test.de"
                },
                RoomNumber = roomNumber,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2)
            };
        }
    }
}