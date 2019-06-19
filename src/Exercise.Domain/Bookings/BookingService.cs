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
    ///     3. Verify if booking is allowed according to the booking policies defined, if any. See Booking Policy Service for more details. (Done)
    ///     4. Booking should only be allowed if there is at least one room type available during the whole booking period (Done).
    ///     5. Keep track of all bookings. E.g. If hotel has 5 standard rooms, we should have no more than 5 bookings in the same day. (Done)
    ///     6. Hotel rooms can be booked many times as long as there are no conflicts with the dates. (Done) 
    ///     7. Return booking confirmation to the employee or error otherwise (exceptions can also be used). (Done)
    /// </remarks>
    public class BookingService
    {
        private const int OneDay = 1;
        private readonly IBookingPolicyService _bookingPolicyService;
        private readonly IHotelService _hotelService;
        private readonly BookingRepository _bookingRepository;

        public BookingService(
            IBookingPolicyService bookingPolicyService,
            IHotelService hotelService,
            BookingRepository bookingRepository)
        {
            _bookingPolicyService = bookingPolicyService;
            _hotelService = hotelService;
            _bookingRepository = bookingRepository;
        }

        public BookingStatus Book(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn, DateTime checkOut)
        {           
            ValidateBookingDates(checkIn, checkOut);

            BookingStatus result = null;
            var hotel = _hotelService.FindHotelBy(hotelId);
            if (hotel == null)
            {
                result = new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, roomType: roomType, hotelId: hotelId, errors: HotelNotFound);
                _bookingRepository.Add(result);
                return result;
            }

            result = VerifyHotelHasRoomType(employeeId, hotelId, roomType, checkIn, checkOut, hotel);
            if (result != null)
            {
                _bookingRepository.Add(result);
                return result;
            }

            result = VerifyBookingPolicyAllowsBooking(employeeId, hotelId, roomType, checkIn, checkOut);
            if (result != null)
            {
                _bookingRepository.Add(result);
                return result;
            }

            result = VerifyRoomTypeAvailability(employeeId, hotelId, roomType, checkIn, checkOut, hotel);
            if (result != null)
            {
                _bookingRepository.Add(result);
                return result;
            }

            result = new BookingStatus(checkIn, checkOut, employeeId, roomType, hotel.Id);
            _bookingRepository.Add(result);
            return result;
        }

        private BookingStatus VerifyBookingPolicyAllowsBooking(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn,
            DateTime checkOut)
        {
            var isBookingAllowed = _bookingPolicyService.IsBookingAllowed(employeeId, roomType);
            if (!isBookingAllowed)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, roomType: roomType,
                    hotelId: hotelId, errors: BookingPolicyRejection);
            }

            return null;
        }

        private BookingStatus VerifyRoomTypeAvailability(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn,
            DateTime checkOut, Hotel hotel)
        {
            var bookedRooms = _bookingRepository.BookingsBetween(checkIn, checkOut, roomType);
            var quantity = hotel.QuantityOfRooms(roomType);
            var availableRooms = quantity - bookedRooms.Count;
            if (availableRooms < 1)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, roomType: roomType,
                    hotelId: hotelId,
                    errors: $"The hotel has '{bookedRooms.Count}' booked rooms and no available rooms.");
            }

            return null;
        }

        private static BookingStatus VerifyHotelHasRoomType(Guid employeeId, Guid hotelId, Guid roomType, DateTime checkIn,
            DateTime checkOut, Hotel hotel)
        {
            var doesHotelHaveRoomType = hotel.HasRoomType(roomType);
            if (!doesHotelHaveRoomType)
            {
                return new BookingStatus(startDate: checkIn, endDate: checkOut, guestId: employeeId, roomType: roomType,
                    hotelId: hotelId, errors: $"Room type '{roomType}' does not exist within hotel '{hotelId}'.");
            }

            return null;
        }

        private static void ValidateBookingDates(DateTime checkIn, DateTime checkOut)
        {
            if (checkOut <= checkIn)
            {
                throw new NotSupportedException(CheckoutLessThanCheckinDate);
            }

            if (checkOut.Subtract(checkIn).Days < OneDay)
            {
                throw new NotSupportedException(CheckoutMustBeGreaterAndEqualToADay);
            }
        }
    }
}
