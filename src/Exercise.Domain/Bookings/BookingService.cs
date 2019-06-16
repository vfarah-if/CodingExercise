using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;
using System;
using static Exercise.Domain.ErrorMessages;

namespace Exercise.Domain.Bookings
{
    /// <summary>
    ///     Allows employees to book rooms at hotels. 
    /// </summary>
    /// <remarks>
    ///     1. Check out date must be at least one day after the check in date (Done).
    ///     2. Validate if the hotel exists and room type is provided by the hotel (Done).    
    ///     3. Verify if booking is allowed according to the booking policies defined, if any. See Booking Policy Service for more details.
    ///     4. Verify Employee exists (Added)
    ///     5. Booking should only be allowed if there is at least one room type available during the whole booking period.
    ///     6. Keep track of all bookings. E.g. If hotel has 5 standard rooms, we should have no more than 5 bookings in the same day.
    ///     7. Hotel rooms can be booked many times as long as there are no conflicts with the dates.
    ///     8. Return booking confirmation to the employee or error otherwise (exceptions can also be used).
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
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, errors: CheckoutLessThanCheckinDate);
            }

            if (checkOut.Subtract(checkIn).Days < OneDay)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, errors: CheckoutMustBeGreaterAndEqualToADay);
            }

            var hotel = _hotelService.FindHotelBy(hotelId);
            if (hotel == null)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, errors: HotelNotFound);
            }


            return new BookingStatus(startDate: checkIn, endDate: checkOut, hotel: hotel, guestId: employeeId, errors: "TODO: Keep test failing for valid reasons");
        }
    }
}
