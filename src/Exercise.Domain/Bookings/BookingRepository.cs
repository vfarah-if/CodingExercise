using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain.Bookings
{
    public class BookingRepository : InMemoryRepository<BookingStatus>
    {
        public virtual IReadOnlyList<BookingStatus> BookingsBetween(DateTime startDate, DateTime endDate, Guid roomType)
        {
            return Entities
                .OrderBy(x => startDate)
                .Where(x => x.IsBooked && 
                            x.RoomType == roomType && 
                            x.StartDate <= endDate && 
                            startDate < x.EndDate)
                .ToList()
                .AsReadOnly();
        }
    }
}
