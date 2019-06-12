using System;

namespace Exercise.Domain.Company
{
    /// <summary>
    ///     Enables company admins to add and delete employees. 
    /// </summary>
    /// <remarks>
    ///     Rules:
    ///         1. Employees should not be duplicated.
    ///         2. When deleting an employee, all the bookings and policies associated to the employee should also be deleted from the system. 
    /// </remarks>
    public class CompanyService
    {
        public void AddEmployee(Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
