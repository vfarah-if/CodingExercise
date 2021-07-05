using System;
using Exercise.Domain.Hotels;
using Moq;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Hotels
{
    public class HotelServiceShould
    {
        private readonly HotelService _hotelService;
        private readonly Mock<HotelRepository> _hotelRepositoryMock;

        public HotelServiceShould()
        {
            _hotelRepositoryMock = new Mock<HotelRepository>();
            _hotelService = new HotelService(_hotelRepositoryMock.Object);
        }

        [Fact]
        public void AddRoomTypeInformationToTheHotelRepository()
        {
            Guid hotelId = Guid.NewGuid();
            Guid roomTypeId = Guid.NewGuid(); 
            int quantity = 4;

            _hotelService.SetRoomType(hotelId, roomTypeId, quantity);

            _hotelRepositoryMock.Verify(x => x.AddRoomType(hotelId, roomTypeId, quantity), Times.Once());
        }

        [Fact]
        public void FindHotelUsingTheHotelRepository()
        {
            Guid hotelId = Guid.NewGuid();
            Guid roomTypeId = Guid.NewGuid();
            int quantity = 4;
            _hotelService.SetRoomType(hotelId, roomTypeId, quantity);

            _hotelService.FindHotelBy(hotelId);

            _hotelRepositoryMock.Verify(x => x.GetBy(hotelId), Times.Once());
        }
    }
}
