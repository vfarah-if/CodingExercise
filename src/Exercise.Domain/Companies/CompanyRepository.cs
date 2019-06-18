using System;

namespace Exercise.Domain.Companies
{
    public class CompanyRepository : InMemoryRepository<Company>
    {

        public virtual void AddEmployee(Guid id, Guid employeeId)
        {
            var company = GetBy(id) ?? Add(new Company(id));
            if (!company.HasEmployee(employeeId))
            {
                company.AddEmployee(employeeId);
            }
        }
    }
}
