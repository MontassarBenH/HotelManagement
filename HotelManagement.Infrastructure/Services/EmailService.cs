
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HotelManagement.Application.Models;

namespace HotelManagement.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(Booking booking);
    }

    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
        }

        public async Task SendBookingConfirmationAsync(Booking booking)
        {
            if (string.IsNullOrEmpty(booking.Guest?.Email))
                return;

            var subject = $"Buchungsbestätigung - Buchungs-Nr. {booking.BookingId}";
            var body = $@"
                <html>
                <body>
                    <h2>Ihre Buchungsbestätigung</h2>
                    <p>Sehr geehrte/r {booking.Guest.Name},</p>
                    <p>Ihre Buchung wurde erfolgreich erstellt:</p>
                    <ul>
                        <li><strong>Buchungs-Nr:</strong> {booking.BookingId}</li>
                        <li><strong>Zimmer:</strong> {booking.RoomNumber}</li>
                        <li><strong>Check-in:</strong> {booking.CheckInDate:dd.MM.yyyy}</li>
                        <li><strong>Check-out:</strong> {booking.CheckOutDate:dd.MM.yyyy}</li>
                        <li><strong>Anzahl Nächte:</strong> {booking.NumberOfNights}</li>
                        <li><strong>Gesamtpreis:</strong> {booking.TotalPrice:C}</li>
                    </ul>
                    <p>Wir freuen uns auf Ihren Besuch!</p>
                    <p>Mit freundlichen Grüßen,<br/>Ihr Hotel-Team</p>
                </body>
                </html>
            ";

            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                var message = new MailMessage
                {
                    From = new MailAddress(_senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(booking.Guest.Email);

                await client.SendMailAsync(message);
            }
        }
    }
}