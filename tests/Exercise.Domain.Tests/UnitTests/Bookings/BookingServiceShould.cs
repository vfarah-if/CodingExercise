using Exercise.Domain.Bookings;
using Exercise.Domain.Hotels;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingServiceShould
    {
        private readonly Mock<IBookingPolicyService> _bookingPolicyServiceMock;
        private readonly Mock<IHotelService> _hotelServiceMock;
        private readonly BookingService _bookingService;
        private readonly Mock<BookingRepository> _bookingRepository;
        private Guid _employeeId;
        private Guid _hotelId;
        private Guid _roomType;
        private Hotel _hotelExistsResponse;
        private DateTime _checkIn;
        private DateTime _checkout;
        private bool _isBookingAllowedResponse;
        private List<BookingStatus> _bookingResponse;

        public BookingServiceShould()
        {
            _checkIn = DateTime.Now;
            _checkout = DateTime.Now.AddDays(1);

            _bookingPolicyServiceMock = new Mock<IBookingPolicyService>();
            _isBookingAllowedResponse = true;
            _bookingPolicyServiceMock
                .Setup(x => x.IsBookingAllowed(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(() => _isBookingAllowedResponse);

            _bookingRepository = new Mock<BookingRepository>();
            _bookingResponse = new List<BookingStatus>();
            _bookingRepository
                .Setup(x => x.BookingsBetween(_checkIn, _checkout, _roomType))
                .Returns(() => _bookingResponse.AsReadOnly());


            _hotelServiceMock = new Mock<IHotelService>();
            _hotelId = Guid.NewGuid();
            _roomType = Guid.NewGuid();
            _hotelExistsResponse = new Hotel(_hotelId);
            _hotelExistsResponse.AddRoomType(_roomType, 1);
            _hotelServiceMock
                .Setup(x => x.FindHotelBy(_hotelId))
                .Returns(() => _hotelExistsResponse);

            _bookingService = new BookingService(
                _bookingPolicyServiceMock.Object,
                _hotelServiceMock.Object,
                _bookingRepository.Object);
        }


        [Fact]
        public void ThrowNotSupportedExceptionWhenCheckoutDateIsNotLessThanOrEqualToCheckinDate()
        {
            var checkIn = DateTime.Now;
            var checkout = DateTime.Now.AddMinutes(-1);

            Action actual = () => _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.Should()
                .Throw<NotSupportedException>()
                .WithMessage("Check-in date can not be less than or equal to check-out date.");
        }

        [Fact]
        public void ThrowNotSupportedExceptionWhenCheckoutDateIsGreaterThanOrEqualsTo1Day()
        {
            var checkIn = DateTime.Now;
            var checkout = DateTime.Now.AddHours(23);

            Action actual = () => _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.Should().Throw<NotSupportedException>().WithMessage("Check-out must be at least 1 day after Check-in date.");
        }

        [Fact]
        public void NotAllowBookingWhenHotelDoesNotExist()
        {
            _hotelExistsResponse = null;

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, _checkIn, _checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Hotel does not exist.");
        }

        [Fact]
        public void NotAllowBookingWhenHotelDoesNotSupportRoomType()
        {
            Guid nonExistentRoomType = Guid.NewGuid();
            var actual = _bookingService.Book(_employeeId, _hotelId, nonExistentRoomType, _checkIn, _checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be($"Room type '{nonExistentRoomType}' does not exist within hotel '{_hotelId}'.");
        }

        [Fact]
        public void NotAllowAnEmployeeToBookWhenPolicyDoesNotPermit()
        {
            _isBookingAllowedResponse = false;
            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, _checkIn, _checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Booking is declined as the booking policy does not allow the employee to book this room type.");
        }

        [Fact]
        public void NotAllowAnEmployeeToBookWhenTheRoomTypeIsFullyBooked()
        {
            // Setup that a booking already exists using the full quota
            var bookingStatus = new BookingStatus(_checkIn, _checkout, _employeeId, _roomType, _hotelId);
            _bookingResponse.Add(bookingStatus);

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, _checkIn, _checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("The hotel has '1' booked rooms and no available rooms.");
        }
    }
}
