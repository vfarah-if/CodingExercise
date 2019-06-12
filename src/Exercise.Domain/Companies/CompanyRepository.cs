using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain.Companies
{
    public class CompanyRepository
    {
        private readonly List<Company> _companies = new List<Company>();

        public virtual IReadOnlyList<Company> List()
        {
            return _companies.AsReadOnly();
        }

        public Company Add(Guid? companyId = null)
        {
            var result = new Company(companyId);
            _companies.Add(result);
            return result;
        }

        public Company GetBy(Guid companyId)
        {
            return _companies.SingleOrDefault(x => x.Id == companyId);
        }

        public virtual void AddEmployee(Guid companyId, Guid employeeId)
        {
            var company = GetBy(companyId) ?? Add(companyId);
            if (!company.HasEmployee(employeeId))
            {
                company.AddEmployee(employeeId);
            }
        }
    }
}
