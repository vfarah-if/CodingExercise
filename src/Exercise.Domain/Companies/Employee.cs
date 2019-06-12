using System;

namespace Exercise.Domain.Companies
{
    public class Employee
    {
        public Employee(Guid companyId, Guid id)
        {
            this.Id = id;
            this.CompanyId = companyId;
        }
        public Guid CompanyId { get; }
        public Guid Id { get; }
    }
}
