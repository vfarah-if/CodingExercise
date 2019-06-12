using System;
using System.Collections.Generic;

namespace Exercise.Domain.Companies
{
    public class CompanyRepository
    {

        public virtual IReadOnlyList<Company> List()
        {
            throw new NotImplementedException();
        }

        public Company Add()
        {
            throw new NotImplementedException();
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
