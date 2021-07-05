using System;
using System.Collections.Generic;

namespace Exercise.Domain.Bookings
{
    public interface IBookingPolicyService
    {
        bool IsBookingAllowed(Guid employeeId, Guid roomType);
        void SetCompanyPolicy(Guid companyId, IReadOnlyList<Guid> roomTypes);
        void SetEmployeePolicy(Guid employeeId, IReadOnlyList<Guid> roomTypes);
    }
}