using System;
using System.Linq;

namespace Exercise.Domain.Bookings
{
    public class BookingPolicyRepository : InMemoryRepository<BookingPolicy>
    {
        public override BookingPolicy Add(Guid? id = null)
        {
            var result = new BookingPolicy(id);
            Entities.Add(result);
            return result;
        }

        public virtual BookingPolicy AddOrUpdate(Guid? id, params Guid[] allowedRoomTypes)
        {
            if (allowedRoomTypes == null)
            {
                throw new ArgumentNullException(nameof(allowedRoomTypes));
            }

            BookingPolicy result = null;
            if (id.HasValue)
            {
                result = GetBy(id.Value) ?? Add(id);
            }
            else
            {
                result = Add();
            }
           
            result.AddRoomTypes(allowedRoomTypes.ToArray());
            return result;
        }
    }
}
