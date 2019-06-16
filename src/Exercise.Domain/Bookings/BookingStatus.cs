using System;
using System.Linq;
using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;

namespace Exercise.Domain.Bookings
{
    public class BookingStatus : GuidEntity
    {
        public BookingStatus(
            DateTime startDate,
            DateTime endDate,
            Employee guest = null, 
            Hotel hotel = null,             
            Guid? id = null,
            params string[] errors) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;
            Guest = guest;
            Errors = errors;
            Hotel = hotel;
        }

        public bool IsBooked => Errors == null || !Errors.Any();
        public Hotel Hotel { get; }
        public Employee Guest { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get;  }
        public string[] Errors { get; }
    }
}
