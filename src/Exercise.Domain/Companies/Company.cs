using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain.Companies
{
    public class Company
    {
        private List<Employee> _employees;

        public Company()
        {
            _employees = new List<Employee>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public Employee GetEmployee(Guid employeeId)
        {
            return _employees.SingleOrDefault(item => item.Id == employeeId);
        }
    }
}
