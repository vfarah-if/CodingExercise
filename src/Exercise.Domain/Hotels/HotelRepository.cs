using System;
using System.Linq;

namespace Exercise.Domain.Hotels
{
    public class HotelRepository : InMemoryRepository<Hotel>
    {
        public override Hotel Add(Guid? id = null)
        {
            var result = new Hotel(id);
            Entities.Add(result);
            return result;
        }

        public override Hotel GetBy(Guid id)
        {
            return Entities.SingleOrDefault(x => x.Id == id);
        }

        public virtual Hotel AddRoomType(Guid hotelId, Guid roomTypeId, int quantity)
        {
            var hotel = GetBy(hotelId) ?? Add(hotelId);
            hotel.AddRoomType(roomTypeId, quantity);
            return hotel;
        }
    }
}