using System;

namespace Exercise.Domain.Companies
{
    /// <summary>
    ///     Enables company admins to add and delete employees. 
    /// </summary>
    /// <remarks>
    ///     Rules:
    ///         1. Employees should not be duplicated (Done).
    ///         2. When deleting an employee, all the bookings and policies associated to the employee should also be deleted from the system.
    ///         *3*. Needed to find a company with employees
    /// </remarks>
    public class CompanyService : ICompanyService
    {
        private readonly CompanyRepository _companyRepository;

        public CompanyService(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }      

        public void AddEmployee(Guid companyId, Guid employeeId)
        {
            _companyRepository.AddEmployee(companyId, employeeId);
        }

        public void DeleteEmployee(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        // CHECK: If this is needed or why I added this
        public Company FindCompany(Guid companyId)
        {
            return _companyRepository.GetBy(companyId);
        }
    }
}
