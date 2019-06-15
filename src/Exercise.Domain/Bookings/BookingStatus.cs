using System;
using Exercise.Domain.Companies;
using Exercise.Domain.Hotels;

namespace Exercise.Domain.Bookings
{
    public class BookingStatus : GuidEntity
    {
        public BookingStatus(Employee guest, Hotel hotel, DateTime startDate, DateTime endDate, bool isBooked = true, Guid? id = null) : base(id)
        {
            Guest = guest;
            StartDate = startDate;
            EndDate = endDate;
            IsBooked = isBooked;
            Hotel = hotel;
        }

        public bool IsBooked { get; }
        public Hotel Hotel { get; }
        public Employee Guest { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get;  }        
    }
}
