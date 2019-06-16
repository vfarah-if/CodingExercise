using System;
using System.Linq;
using Exercise.Domain.Bookings;
using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;
using FluentAssertions;
using Moq;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingServiceShould
    {
        private readonly Mock<IBookingPolicyService> _bookingServiceMock;
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly Mock<IHotelService> _hotelServiceMock;
        private readonly BookingService _bookingService;
        private Guid _employeeId;
        private Guid _hotelId;
        private Guid _roomType;
        private Hotel _hotelExistsResponse;
        private DateTime _checkIn;
        private DateTime _checkout;

        public BookingServiceShould()
        {
            _checkIn = DateTime.Now;
            _checkout = DateTime.Now.AddDays(1);

            _bookingServiceMock = new Mock<IBookingPolicyService>();
            _companyServiceMock = new Mock<ICompanyService>();

            _hotelServiceMock = new Mock<IHotelService>();
            _hotelId = Guid.NewGuid();
            
            _hotelServiceMock.Setup(x => x.FindHotelBy(_hotelId)).Returns(() => _hotelExistsResponse);

            _bookingService = new BookingService(
                _bookingServiceMock.Object, 
                _companyServiceMock.Object,
                _hotelServiceMock.Object);
        }


        [Fact]
        public void VerifyCheckoutDateIsNotLessThanOrEqualToCheckinDate()
        {
            var checkIn = DateTime.Now;
            var checkout = DateTime.Now.AddMinutes(-1);

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Check-in date can not be less than or equal to check-out date.");
        }

        [Fact]
        public void VerifyCheckoutDateIsGreaterThanOrEqualsTo1Day()
        {
            var checkIn = DateTime.Now;
            var checkout = DateTime.Now.AddHours(23);

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Check-out must be at least 1 day after Check-in date.");
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
    }
}
