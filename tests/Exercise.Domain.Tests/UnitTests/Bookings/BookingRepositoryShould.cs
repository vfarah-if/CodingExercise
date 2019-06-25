using System;
using System.Linq;
using Exercise.Domain.Bookings;
using FluentAssertions;
using Xunit;
using static Exercise.Domain.Bookings.BookingStatus;

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
            BookingStatus bookingStatus = CreateStatus(
                startDate: DateTime.Now, endDate: DateTime.Now.AddDays(2), 
                guestId: Guid.NewGuid(), roomType: Guid.NewGuid(), hotelId: Guid.NewGuid());

            _bookingRepository.Add(bookingStatus);

            _bookingRepository.Exists(bookingStatus).Should().BeTrue();
        }

        [Fact]
        public void FindTwoBookingsBetweenACertainDateWhenTheyOverlap()
        {
            Guid roomType = Guid.NewGuid();
            BookingStatus bookingStatus = CreateStatus(
                startDate: DateTime.Now, endDate: DateTime.Now.AddDays(2),
                guestId: Guid.NewGuid(), roomType:roomType, hotelId: Guid.NewGuid());
            BookingStatus overlappedBookingStatus = CreateStatus(
                startDate: DateTime.Now.AddDays(1), endDate: DateTime.Now.AddDays(3),
                guestId: Guid.NewGuid(), roomType: roomType, hotelId: Guid.NewGuid());
            _bookingRepository.Add(bookingStatus);
            _bookingRepository.Add(overlappedBookingStatus);

            var actual = _bookingRepository.BookingsBetween(DateTime.Now, DateTime.Now.AddDays(1), roomType);

            actual.Count().Should().Be(2);
        }

        [Fact]
        public void FindOneBookingsBetweenACertainDateWhenThereIsNoOverlap()
        {
            Guid roomType = Guid.NewGuid();
            BookingStatus bookingStatus = CreateStatus(
                startDate: DateTime.Now, endDate: DateTime.Now.AddDays(2),
                guestId: Guid.NewGuid(), roomType: roomType, hotelId: Guid.NewGuid());
            BookingStatus overlappedBookingStatus = CreateStatus(
                startDate: DateTime.Now.AddDays(2), endDate: DateTime.Now.AddDays(3),
                guestId: Guid.NewGuid(), roomType: roomType, hotelId: Guid.NewGuid());
            _bookingRepository.Add(bookingStatus);
            _bookingRepository.Add(overlappedBookingStatus);

            var actual = _bookingRepository.BookingsBetween(DateTime.Now, DateTime.Now.AddDays(1), roomType);

            actual.Count().Should().Be(1);
        }
    }
}
