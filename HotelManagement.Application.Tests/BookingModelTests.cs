using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;
using HotelManagement.Application.Models;

namespace HotelManagement.Application.Tests
{
    [TestClass]
    public class BookingModelTests
    {
        [TestMethod]
        public void TotalPrice_CalculatesCorrectly()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = "B001",
                CheckInDate = new DateTime(2025, 10, 1),
                CheckOutDate = new DateTime(2025, 10, 5),
                DailyRate = 100.00m
            };

            // Act
            var totalPrice = booking.TotalPrice;

            // Assert
            totalPrice.Should().Be(400.00m);
            booking.NumberOfNights.Should().Be(4);
        }

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenCheckInAfterCheckOut()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = "B001",
                Guest = new Guest { Id = "G001", Name = "Test" },
                RoomNumber = "101",
                CheckInDate = new DateTime(2025, 10, 5),
                CheckOutDate = new DateTime(2025, 10, 1)
            };

            // Act
            var isValid = booking.IsValid();

            // Assert
            isValid.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_ReturnsTrue_WhenAllDataCorrect()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = "B001",
                Guest = new Guest { Id = "G001", Name = "Max Mustermann" },
                RoomNumber = "101",
                CheckInDate = new DateTime(2025, 10, 1),
                CheckOutDate = new DateTime(2025, 10, 5)
            };

            // Act
            var isValid = booking.IsValid();

            // Assert
            isValid.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenGuestMissing()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = "B001",
                Guest = null,
                RoomNumber = "101",
                CheckInDate = new DateTime(2025, 10, 1),
                CheckOutDate = new DateTime(2025, 10, 5)
            };

            // Act
            var isValid = booking.IsValid();

            // Assert
            isValid.Should().BeFalse();
        }
    }
}