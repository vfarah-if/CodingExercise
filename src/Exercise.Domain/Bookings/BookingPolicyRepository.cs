using System;
using System.Linq;

namespace Exercise.Domain.Bookings
{
    public class BookingPolicyRepository : InMemoryRepository<BookingPolicy>
    {

        public virtual BookingPolicy AddOrUpdate(Guid? id, params Guid[] allowedRoomTypes)
        {
            if (allowedRoomTypes == null)
            {
                throw new ArgumentNullException(nameof(allowedRoomTypes));
            }

            BookingPolicy result = null;
            if (id.HasValue)
            {
                result = GetBy(id.Value) ?? Add(new BookingPolicy(id));
            }
            else
            {
                result = Add(new BookingPolicy(null));
            }
           
            result.AddRoomTypes(allowedRoomTypes.ToArray());
            return result;
        }
    }
}
