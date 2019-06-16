using System;
using System.Collections.Generic;
using System.Linq;

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
    ///         1. Employee policies take precedence over company policies. (Done)
    ///         2. If there is a policy for an employee, the policy should be respected regardless of what the company policy (if any) says. (Done)
    ///             Comment: - no need to repeat this as this is  understood in 1.
    ///         3. If an employee policy does not exist, the company policy should be checked. (Done)
    ///         4. If neither employee nor company policies exist, the employee should be allowed to book any room. (Done)
    /// 
    ///     Technical Rules
    ///         1. Methods `setCompanyPolicy(...)` and `setEmployeePolicy(...)` should create a new policy or update an existing one. (Done)
    ///         2. No duplicate company or employee policies are allowed. (Done)
    /// </remarks>
    public class BookingPolicyService : IBookingPolicyService
    {
        private readonly BookingPolicyRepository _employeeBookingPolicyRepository;
        private readonly BookingPolicyRepository _companyBookingPolicyRepository;

        public BookingPolicyService(BookingPolicyRepository employeeBookingPolicyRepository,
            BookingPolicyRepository companyBookingPolicyRepository)
        {
            _employeeBookingPolicyRepository = employeeBookingPolicyRepository;
            _companyBookingPolicyRepository = companyBookingPolicyRepository;
        }

        public void SetCompanyPolicy(Guid companyId, IReadOnlyList<Guid> roomTypes)
        {
            if (roomTypes == null)
            {
                throw new ArgumentNullException(nameof(roomTypes));
            }

            _companyBookingPolicyRepository.AddOrUpdate(companyId, roomTypes.ToArray());
        }

        public void SetEmployeePolicy(Guid employeeId, IReadOnlyList<Guid> roomTypes)
        {
            if (roomTypes == null)
            {
                throw new ArgumentNullException(nameof(roomTypes));
            }

            _employeeBookingPolicyRepository.AddOrUpdate(employeeId, roomTypes.ToArray());
        }

        public bool IsBookingAllowed(Guid employeeId, Guid roomType)
        {
            return HasEmployeeRoomTypePolicy(employeeId, roomType) ||
                   HasCompanyRoomTypePolicy(employeeId, roomType);
        }

        private bool HasCompanyRoomTypePolicy(Guid employeeId, Guid roomType)
        {
            return !_companyBookingPolicyRepository.List().Any() ||
                   _companyBookingPolicyRepository.GetBy(employeeId).RoomTypes.Contains(roomType);
        }

        private bool HasEmployeeRoomTypePolicy(Guid employeeId, Guid roomType)
        {
            return !_employeeBookingPolicyRepository.List().Any() ||
                   _employeeBookingPolicyRepository.GetBy(employeeId).RoomTypes.Contains(roomType);
        }
    }
}
