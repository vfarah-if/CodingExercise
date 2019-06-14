using System;
using System.Diagnostics;
using AutoFixture;
using CoreBDD;
using Exercise.Domain.Hotels;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class HotelManagerShould: HotelsManagementFeature
    {
        private Fixture _autoFixture;
        private HotelService _hotelService;
        private Guid _roomType;
        private Guid _hotelId;
        private int _quantity;

        public HotelManagerShould()
        {
            _hotelService = new HotelService();
            _autoFixture = new Fixture();
        }

        [Scenario("Define the number of room types a hotel supports")]
        public void SetRoomTypesAndHowManyAreOnOffer()
        {
            Given("a hotel id, room type and quantity", () =>
            {
                _roomType = Guid.NewGuid();
                _hotelId = Guid.NewGuid();
                _quantity = _autoFixture.Create<int>();
            });
            When("setting the room type information", () =>
            {
                _hotelService.SetRoomType(_hotelId, _roomType, _quantity);
            });
            Then("expect the hotel to include the all provided information", () =>
            {
                var hotel = _hotelService.FindHotelBy(_hotelId);
                hotel.Should().NotBeNull();
                hotel.HasRoomType(_roomType).Should().BeTrue();
                hotel.QuantityOfRooms(_roomType).Should().Be(_quantity);
            });
        }
    }
}
