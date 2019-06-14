using System;
using System.Collections.Generic;

namespace Exercise.Domain.Bookings
{
    public class BookingPolicy : GuidEntity
    {
        private readonly List<Guid> _roomTypes;

        public BookingPolicy(Guid? Id)
            : base(Id)
        {
            this._roomTypes = new List<Guid>();
        }

        public IReadOnlyList<Guid> RoomTypes => _roomTypes.AsReadOnly();

        public void AddRoomTypes(params Guid[] roomTypes)
        {
            _roomTypes.AddRange(roomTypes);
        }
    }
}
