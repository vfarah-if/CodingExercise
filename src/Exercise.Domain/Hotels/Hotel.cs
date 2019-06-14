using System;
using System.Collections.Generic;

namespace Exercise.Domain.Hotels
{
    public class Hotel : GuidEntity
    {
        private readonly Dictionary<Guid, int> _roomTypes = new Dictionary<Guid, int>();

        public Hotel(Guid? id = null) : base(id)
        {            
        }

        public bool HasRoomType(Guid roomType)
        {
            return _roomTypes.ContainsKey(roomType);
        }

        public int QuantityOfRooms(Guid roomType)
        {
            return _roomTypes[roomType];
        }

        public void AddRoomType(Guid roomTypeId, int quantity)
        {
            _roomTypes[roomTypeId] = quantity;
        }
    }
}
