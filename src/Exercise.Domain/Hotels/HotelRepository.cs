using System;

namespace Exercise.Domain.Hotels
{
    public class HotelRepository : InMemoryRepository<Hotel>
    {

        public virtual Hotel AddRoomType(Guid hotelId, Guid roomTypeId, int quantity)
        {
            var hotel = GetBy(hotelId) ?? Add(new Hotel(hotelId));
            hotel.AddRoomType(roomTypeId, quantity);
            return hotel;
        }
    }
}