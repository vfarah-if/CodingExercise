using System;

namespace Exercise.Domain.Hotels
{
    public interface IHotelService
    {
        Hotel FindHotelBy(Guid hotelId);
        void SetRoomType(Guid hotelId, Guid roomTypeId, int quantity);
    }
}