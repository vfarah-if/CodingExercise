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

        public BookingServiceShould()
        {
            _bookingServiceMock = new Mock<IBookingPolicyService>();
            _companyServiceMock = new Mock<ICompanyService>();
            _hotelServiceMock = new Mock<IHotelService>();

            _bookingService = new BookingService(
                _bookingServiceMock.Object, 
                _companyServiceMock.Object,
                _hotelServiceMock.Object);
        }


        [Fact]
        public void VerifyCheckoutDateIsNotLessThanOrEqual()
        {
            DateTime checkIn = DateTime.Now;
            DateTime checkout = DateTime.Now;

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Check-in date can not be less than or equal to check-out date.");
        }

        [Fact]
        public void VerifyCheckoutDateIsGreaterThanOrEqualsTo24Hours()
        {
            DateTime checkIn = DateTime.Now;
            DateTime checkout = DateTime.Now.AddHours(23);

            var actual = _bookingService.Book(_employeeId, _hotelId, _roomType, checkIn, checkout);

            actual.IsBooked.Should().BeFalse();
            actual.Errors.Length.Should().Be(1);
            actual.Errors.First().Should().Be("Check-out must be at least 24 hours after Check-in date.");
        }
    }
}
