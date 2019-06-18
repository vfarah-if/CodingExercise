using System;
using Exercise.Domain.Bookings;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Bookings
{
    public class BookingRepositoryShould
    {
        private readonly BookingRepository _bookingRepository;

        public BookingRepositoryShould()
        {
            _bookingRepository = new BookingRepository();
        }

        [Fact]
        public void AddABookingStatusRepository()
        {
            BookingStatus bookingStatus = new BookingStatus(DateTime.Now, DateTime.Now.AddDays(2), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            _bookingRepository.Add(bookingStatus);

            _bookingRepository.Exists(bookingStatus).Should().BeTrue();
        }
    }
}
