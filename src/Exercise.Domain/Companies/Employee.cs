using System;

namespace Exercise.Domain.Companies
{
    public class Employee: GuidEntity
    {
        public Employee(Guid companyId, Guid id) :base(id)
        {
            this.CompanyId = companyId;
        }

        public Guid CompanyId { get; }
        
    }
}
