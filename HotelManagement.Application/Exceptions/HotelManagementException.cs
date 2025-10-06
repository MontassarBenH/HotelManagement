using System;

namespace HotelManagement.Application.Exceptions
{
    public class HotelManagementException : Exception
    {
        public string ErrorCode { get; }

        public HotelManagementException(string message, string errorCode = null)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public HotelManagementException(string message, Exception innerException, string errorCode = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }

    public class RoomNotFoundException : HotelManagementException
    {
        public RoomNotFoundException(string roomNumber)
            : base($"Zimmer '{roomNumber}' wurde nicht gefunden", "ROOM_NOT_FOUND")
        {
        }
    }

    public class RoomNotAvailableException : HotelManagementException
    {
        public RoomNotAvailableException(string roomNumber, DateTime checkIn, DateTime checkOut)
            : base($"Zimmer '{roomNumber}' ist nicht verfügbar vom {checkIn:dd.MM.yyyy} bis {checkOut:dd.MM.yyyy}", "ROOM_NOT_AVAILABLE")
        {
        }
    }
}