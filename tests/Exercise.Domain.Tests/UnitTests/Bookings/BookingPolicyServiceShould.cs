using Exercise.Domain.Bookings;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingPolicyServiceShould
    {
        private readonly BookingPolicyService _bookingPolicyService;
        private Mock<BookingPolicyRepository> _employeeBookingPolicyRepositoryMock;

        public BookingPolicyServiceShould()
        {
            _employeeBookingPolicyRepositoryMock = new Mock<BookingPolicyRepository>();
            _bookingPolicyService = new BookingPolicyService(_employeeBookingPolicyRepositoryMock.Object);
        }

        [Fact]
        public void ShouldAllowBookingWhenNoPoliciesAssociated()
        {
            Guid employeeId = Guid.NewGuid();
            Guid roomType = Guid.NewGuid();

            _bookingPolicyService.IsBookingAllowed(employeeId, roomType).Should().BeTrue();
        }

        [Fact]
        public void ShouldAllowBookingWhenEmployerPolicyIsSet()
        {
            Guid employeeId = Guid.NewGuid();
            Guid roomType = Guid.NewGuid();
            BookingPolicy bookingPolicy = new BookingPolicy(employeeId);
            bookingPolicy.AddRoomTypes(roomType);
            IReadOnlyList<BookingPolicy> response = new List<BookingPolicy>(new []{ bookingPolicy });
            _employeeBookingPolicyRepositoryMock.Setup(x => x.List()).Returns(response);
            _employeeBookingPolicyRepositoryMock.Setup(x => x.GetBy(employeeId)).Returns(response.First());

            _bookingPolicyService.SetEmployeePolicy(employeeId, new[] { roomType });

            _bookingPolicyService.IsBookingAllowed(employeeId, roomType).Should().BeTrue();
        }
    }
}
