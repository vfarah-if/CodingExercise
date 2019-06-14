using System;
using Exercise.Domain.Bookings;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingPolicyServiceShould
    {
        private readonly BookingPolicyService _bookingPolicyService;

        public BookingPolicyServiceShould()
        {
            _bookingPolicyService = new BookingPolicyService();    
        }

        [Fact]
        public void ShouldAllowBookingWhenNoPoliciesAssociated()
        {
            Guid employeeId = Guid.NewGuid();
            Guid roomType = Guid.NewGuid();

            var actual = _bookingPolicyService.IsBookingAllowed(employeeId, roomType);

            actual.Should().BeTrue();
        }
    }
}
