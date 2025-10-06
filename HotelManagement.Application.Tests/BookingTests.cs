using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Tests.Models
{
    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        [TestCategory("BusinessRules")]
        public void IsValid_AllFieldsCorrect_ReturnsTrue()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = "B001",
                Guest = new Guest { Id = "G001", Name = "Max Mustermann" },
                RoomNumber = "101",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2)
            };

            // Act
            var isValid = booking.IsValid();

            // Assert
            isValid.Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("BusinessRules")]
        [DataRow(null, "101", "G001")]      // BookingId null
        [DataRow("", "101", "G001")]        // BookingId leer
        [DataRow("B001", null, "G001")]     // RoomNumber null
        [DataRow("B001", "", "G001")]       // RoomNumber leer
        [DataRow("B001", "101", null)]      // GuestId null (Guest null)
        public void IsValid_MissingRequiredFields_ReturnsFalse(string bookingId, string roomNumber, string guestId)
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = bookingId,
                RoomNumber = roomNumber,
                Guest = guestId != null ? new Guest { Id = guestId, Name = "Test" } : null,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };

            // Act
            var isValid = booking.IsValid();

            // Assert
            isValid.Should().BeFalse();
        }

        [TestMethod]
        [TestCategory("BusinessRules")]
        public void NumberOfNights_CalculatesCorrectly()
        {
            // Arrange & Act
            var booking = new Booking
            {
                CheckInDate = new DateTime(2025, 10, 1),
                CheckOutDate = new DateTime(2025, 10, 5)
            };

            // Assert
            booking.NumberOfNights.Should().Be(4);
        }

        [TestMethod]
        [TestCategory("BusinessRules")]
        public void TotalPrice_CalculatesCorrectly()
        {
            // Arrange & Act
            var booking = new Booking
            {
                CheckInDate = new DateTime(2025, 10, 1),
                CheckOutDate = new DateTime(2025, 10, 5),
                DailyRate = 100.00m
            };

            // Assert
            booking.TotalPrice.Should().Be(400.00m);
        }

        [TestMethod]
        [TestCategory("BusinessRules")]
        public void DailyRate_DefaultValue_Is100()
        {
            // Arrange & Act
            var booking = new Booking();

            // Assert
            booking.DailyRate.Should().Be(100.00m);
        }
    }
}