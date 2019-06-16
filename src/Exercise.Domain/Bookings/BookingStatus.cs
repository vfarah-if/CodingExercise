using System;
using System.Linq;
using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;

namespace Exercise.Domain.Bookings
{
    public class BookingStatus : GuidEntity
    {
        protected BookingStatus(
            Employee guest, 
            Hotel hotel, 
            DateTime startDate, 
            DateTime endDate, 
            Guid? id = null,
            params string[] errors) : base(id)
        {
            Guest = guest;
            StartDate = startDate;
            EndDate = endDate;
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
