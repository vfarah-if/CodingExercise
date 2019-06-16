using System;

namespace Exercise.Domain.Companies
{
    public interface ICompanyService
    {
        void AddEmployee(Guid companyId, Guid employeeId);
        void DeleteEmployee(Guid employeeId);        
    }
}