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
        private readonly Mock<BookingPolicyRepository> _employeeBookingPolicyRepositoryMock;
        private readonly Mock<BookingPolicyRepository> _companyBookingPolicyRepositoryMock;

        public BookingPolicyServiceShould()
        {
            _employeeBookingPolicyRepositoryMock = new Mock<BookingPolicyRepository>();
            _companyBookingPolicyRepositoryMock = new Mock<BookingPolicyRepository>();
            _bookingPolicyService = new BookingPolicyService(_employeeBookingPolicyRepositoryMock.Object, _companyBookingPolicyRepositoryMock.Object);
        }

        [Fact]
        public void ShouldAllowBookingWhenNoPoliciesAssociated()
        {
            Guid employeeId = Guid.NewGuid();
            Guid roomType = Guid.NewGuid();
            SetupEmptyBookingResponse();
     
            _bookingPolicyService.IsBookingAllowed(employeeId, roomType).Should().BeTrue();
        }

        [Fact]
        public void ShouldAllowBookingWhenEmployerPolicyIsSet()
        {
            Guid employeeId = Guid.NewGuid();
            Guid roomType = Guid.NewGuid();
            BookingPolicy bookingPolicy = new BookingPolicy(employeeId);
            bookingPolicy.AddRoomTypes(roomType);
            SetupValidBookingResponse(bookingPolicy, employeeId);

            _bookingPolicyService.SetEmployeePolicy(employeeId, new[] { roomType });

            _bookingPolicyService.IsBookingAllowed(employeeId, roomType).Should().BeTrue();
        }

        private void SetupValidBookingResponse(BookingPolicy bookingPolicy, Guid employeeId)
        {
            IReadOnlyList<BookingPolicy> response = new List<BookingPolicy>(new[] {bookingPolicy});
            _employeeBookingPolicyRepositoryMock.Setup(x => x.List()).Returns(response);
            _employeeBookingPolicyRepositoryMock.Setup(x => x.GetBy(employeeId)).Returns(response.First());
        }

        private void SetupEmptyBookingResponse()
        {
            IReadOnlyList<BookingPolicy> emptyResponse = new List<BookingPolicy>();
            _employeeBookingPolicyRepositoryMock.Setup(x => x.List()).Returns(emptyResponse);
        }
    }
}
