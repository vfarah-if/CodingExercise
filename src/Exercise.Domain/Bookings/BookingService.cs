using System;
using Exercise.Domain.Hotels;

namespace Exercise.Domain.Bookings
{
    /// <summary>
    ///     Allows employees to book rooms at hotels. 
    /// </summary>
    /// <remarks>
    ///     1. Check out date must be at least one day after the check in date.
    ///     2. Validate if the hotel exists and room type is provided by the hotel.
    ///     3. Verify if booking is allowed according to the booking policies defined, if any. See Booking Policy Service for more details.
    ///     4. Booking should only be allowed if there is at least one room type available during the whole booking period.
    ///     5. Keep track of all bookings. E.g. If hotel has 5 standard rooms, we should have no more than 5 bookings in the same day.
    ///     6. Hotel rooms can be booked many times as long as there are no conflicts with the dates.
    ///     7. Return booking confirmation to the employee or error otherwise (exceptions can also be used).
    /// </remarks>
    public class BookingService
    {
        public Hotel Book(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn, DateTime checkOut)
        {
            throw new NotImplementedException();
        }
    }
}
