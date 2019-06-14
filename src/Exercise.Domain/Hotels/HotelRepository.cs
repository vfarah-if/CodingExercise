using System;
using Exercise.Domain.Companies;

namespace Exercise.Domain.Hotels
{
    public class HotelRepository : InMemoryRepository<Hotel>
    {
        public override Hotel Add(Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public override Company GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void AddRoomType(Guid hotelId, Guid roomTypeId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}