using System;
using System.Collections.Generic;

namespace Exercise.Domain.Companies
{
    public class CompanyRepository
    {
        private readonly List<Company> _companies = new List<Company>();

        public virtual IReadOnlyList<Company> List()
        {
            throw new NotImplementedException();
        }

        public Company Add()
        {
            var result = new Company();
            _companies.Add(result);
            return result;
        }

        public Company GetBy(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public virtual void AddEmployee(Guid companyId, Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
