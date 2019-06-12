using System;
using System.Collections.Generic;

namespace Exercise.Domain.Bookings
{
    /// <summary>
    ///     Allows company admins to create booking policies for their company and employees. Booking policies 
    ///     determine if an employee booking request is allowed by their company.There are two types of booking policy:
    ///         1. Company Booking Policy: Indicates which type of rooms can be booked. e.g. a company may only allow standard (single/double) rooms 
    ///             to be booked. Or it may allow standard and junior suite rooms.
    ///         2. Employee Booking Policy: Indicates which type of rooms a specific employee can book. E.g. One employee might only be 
    ///             allowed to book a standard room while another employee may be allowed to book standard, junior suite and master suite
    /// </summary>
    /// <remarks>
    ///     Business Rules
    ///         1. Employee policies take precedence over company policies.
    ///         2. If there is a policy for an employee, the policy should be respected regardless of what the company policy (if any) says.
    ///         3. If an employee policy does not exist, the company policy should be checked.
    ///         4. If neither employee nor company policies exist, the employee should be allowed to book any room.
    /// 
    ///     Technical Rules
    ///         1. Methods `setCompanyPolicy(...)` and `setEmployeePolicy(...)` should create a new policy or update an existing one.
    ///         2. No duplicate company or employee policies are allowed.
    /// </remarks>
    public class BookingPolicyService
    {
        void SetCompanyPolicy(Guid companyId, IReadOnlyList<Guid> roomTypes)
        {
            throw new NotImplementedException();
        }

        void SetEmployeePolicy(Guid employeeId, IReadOnlyList<Guid> roomTypes)
        {
            throw new NotImplementedException();
        }

        bool IsBookingAllowed(Guid employeeId, Guid roomType)
        {
            throw new NotImplementedException();
        }
    }
}
