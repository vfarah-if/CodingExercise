using System;

namespace Exercise.Domain.Hotels
{
    public class Hotel : GuidEntity
    {
        public Hotel(Guid? id = null) : base(id)
        {            
        }

        public bool HasRoomType(Guid roomType)
        {
            throw new NotImplementedException();
        }

        public int QuantityOfRooms(Guid roomType)
        {
            throw new NotImplementedException();
        }
    }
}
