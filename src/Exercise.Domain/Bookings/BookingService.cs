using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;
using System;

namespace Exercise.Domain.Bookings
{
    /// <summary>
    ///     Allows employees to book rooms at hotels. 
    /// </summary>
    /// <remarks>
    ///     1. Check out date must be at least one day after the check in date (Done).
    ///     2. Validate if the hotel exists and room type is provided by the hotel (Done).    
    ///     3. Verify if booking is allowed according to the booking policies defined, if any. See Booking Policy Service for more details.
    ///     4. Booking should only be allowed if there is at least one room type available during the whole booking period.
    ///     5. Keep track of all bookings. E.g. If hotel has 5 standard rooms, we should have no more than 5 bookings in the same day.
    ///     6. Hotel rooms can be booked many times as long as there are no conflicts with the dates.
    ///     7. Return booking confirmation to the employee or error otherwise (exceptions can also be used).
    /// </remarks>
    public class BookingService
    {
        private const int OneDay = 1;
        private readonly IBookingPolicyService _bookingPolicyService;
        private readonly ICompanyService _companyService;
        private readonly IHotelService _hotelService;

        public BookingService(
            IBookingPolicyService bookingPolicyService,
            ICompanyService companyService,
            IHotelService hotelService)
        {
            _bookingPolicyService = bookingPolicyService;
            _companyService = companyService;
            _hotelService = hotelService;
        }

        public BookingStatus Book(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn, DateTime checkOut)
        {
            if (checkOut <= checkIn)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, errors: "Check-in date can not be less than or equal to check-out date.");
            }

            if (checkOut.Subtract(checkIn).Days < OneDay)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, errors: "Check-out must be at least 1 day after Check-in date.");
            }

            var hotel = _hotelService.FindHotelBy(hotelId);
            if (hotel == null)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, errors: "Hotel does not exist.");
            }

            return new BookingStatus(startDate: checkIn, endDate: checkOut, hotel: hotel);
        }
    }
}
