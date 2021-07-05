using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain.Hotels
{
    public class Hotel : GuidEntity
    {
        private readonly Dictionary<Guid, int> _roomTypes = new Dictionary<Guid, int>();

        public Hotel(Guid? id = null) : base(id)
        {            
        }

        public int TotalRoomCount => _roomTypes.Sum(x => x.Value);
        public IReadOnlyList<Guid> RoomTypes => _roomTypes.Keys.ToList().AsReadOnly();

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
