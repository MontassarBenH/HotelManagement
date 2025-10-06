using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Models;
using HotelManagement.Application.Interfaces;

namespace HotelManagement.Application.Features.Bookings
{
    public class GetAllBookingsQuery : IRequest<List<Booking>>
    {
    }

    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }
    }
}