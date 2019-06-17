using System;
using System.Linq;

namespace Exercise.Domain.Bookings
{
    public class BookingStatus : GuidEntity
    {
        public BookingStatus(
            DateTime startDate,
            DateTime endDate,
            Guid guestId,
            Guid roomType,
            Guid hotelId,             
            Guid? id = null,
            params string[] errors) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;
            GuestId = guestId;
            RoomType = roomType;
            Errors = errors;
            HotelId = hotelId;
        }

        public bool IsBooked => Errors == null || !Errors.Any();
        public Guid HotelId { get; }
        public Guid GuestId { get; }
        public Guid RoomType { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get;  }
        public string[] Errors { get; }
    }
}
