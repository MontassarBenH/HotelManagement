using FluentValidation;
using HotelManagement.Application.Features.Bookings;
using HotelManagement.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelManagement.Application.Validators
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator(IBookingRepository bookingRepo, IRoomRepository roomRepo)
        {
            RuleFor(x => x.GuestName)
                .NotEmpty().WithMessage("Gastname ist erforderlich")
                .MinimumLength(2).WithMessage("Gastname muss mindestens 2 Zeichen lang sein")
                .MaximumLength(100).WithMessage("Gastname darf maximal 100 Zeichen lang sein");

            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Zimmernummer ist erforderlich")
                .Matches(@"^\d{3}$").WithMessage("Zimmernummer muss 3-stellig sein (z.B. 101)");

            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Check-in Datum darf nicht in der Vergangenheit liegen");

            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate)
                .WithMessage("Check-out Datum muss nach Check-in Datum liegen");

            RuleFor(x => x)
                .Must(x => (x.CheckOutDate - x.CheckInDate).Days <= 30)
                .WithMessage("Buchungszeitraum darf maximal 30 Tage betragen")
                .When(x => x.CheckOutDate > x.CheckInDate);

            RuleFor(x => x).MustAsync(async (cmd, ct) =>
            {
                var all = await bookingRepo.GetAllBookingsAsync();
                return !all.Any(b =>
                    b.RoomNumber == cmd.RoomNumber &&
                    cmd.CheckInDate < b.CheckOutDate &&
                    b.CheckInDate < cmd.CheckOutDate);
            }).WithMessage("Zimmer ist in diesem Zeitraum bereits belegt.");

            RuleFor(x => x).MustAsync(async (cmd, ct) =>
            {
                var room = await roomRepo.GetRoomByNumberAsync(cmd.RoomNumber);
                return room == null || room.IsAvailable; 
            }).WithMessage("Zimmer ist derzeit gesperrt (Wartung).");
        }
    }
}
