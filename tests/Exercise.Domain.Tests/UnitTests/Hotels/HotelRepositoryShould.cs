using System;
using Exercise.Domain.Hotels;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Hotels
{
    public class HotelRepositoryShould
    {
        private readonly HotelRepository _hotelRepository;

        public HotelRepositoryShould()
        {
            _hotelRepository = new HotelRepository();
        }

        [Fact]
        public void AddRoomTypeInformationToHotel()
        {
            Guid hotelId = Guid.NewGuid();
            Guid roomTypeId = Guid.NewGuid();
            int quantity = 7;

            var actual = _hotelRepository.AddRoomType(hotelId, roomTypeId, quantity);

            actual.Should().NotBeNull();
            actual.HasRoomType(roomTypeId).Should().BeTrue();
            actual.QuantityOfRooms(roomTypeId).Should().Be(quantity);
        }
    }
}
