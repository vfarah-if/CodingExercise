using System;
using CoreBDD;
using Exercise.Domain.Bookings;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingPolicyRepositoryShould
    {
        private readonly BookingPolicyRepository _bookingPolicyRepository;

        public BookingPolicyRepositoryShould()
        {
            _bookingPolicyRepository = new BookingPolicyRepository();
        }

        [Fact]
        public void AddABookingPolicy()
        {
            var actual = _bookingPolicyRepository.Add();

            actual.Should().NotBeNull();
            actual.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void AddOrUpdateBookingPolicy()
        {
            var expectedId = Guid.NewGuid();
            var expectedRoomTypeId = Guid.NewGuid();
            var actual = _bookingPolicyRepository.AddOrUpdate(expectedId, expectedRoomTypeId);

            actual.Should().NotBeNull();
            actual.Id.Should().NotBeEmpty();
            actual.Id.Should().Be(expectedId);
            actual.RoomTypes.Should().Contain(expectedRoomTypeId);
        }

        [Fact]
        public void NotAllowDuplicatePolicies()
        {
            var expectedId = Guid.NewGuid();
            var expectedRoomTypeId = Guid.NewGuid();

            _bookingPolicyRepository.AddOrUpdate(expectedId, expectedRoomTypeId);
            _bookingPolicyRepository.AddOrUpdate(expectedId, expectedRoomTypeId);

            _bookingPolicyRepository.List().Count.Should().Be(1);
        }
    }
}
