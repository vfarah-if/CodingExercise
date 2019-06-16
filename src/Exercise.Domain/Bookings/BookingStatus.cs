using System;
using System.Linq;
using Exercise.Domain.Hotels;

namespace Exercise.Domain.Bookings
{
    public class BookingStatus : GuidEntity
    {
        public BookingStatus(
            DateTime startDate,
            DateTime endDate,
            Guid guestId,
            Guid roomType,
            Hotel hotel = null,             
            Guid? id = null,
            params string[] errors) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;
            GuestId = guestId;
            RoomType = roomType;
            Errors = errors;
            Hotel = hotel;
        }

        public bool IsBooked => Errors == null || !Errors.Any();
        public Hotel Hotel { get; }
        public Guid GuestId { get; }
        public Guid RoomType { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get;  }
        public string[] Errors { get; }
    }
}
