using System;

namespace Exercise.Domain.Companies
{
    public class CompanyRepository : InMemoryRepository<Company>
    {
        public override Company Add(Guid? id = null)
        {
            var result = new Company(id);
            Entities.Add(result);
            return result;
        }

        public virtual void AddEmployee(Guid id, Guid employeeId)
        {
            var company = GetBy(id) ?? Add(id);
            if (!company.HasEmployee(employeeId))
            {
                company.AddEmployee(employeeId);
            }
        }
    }
}
