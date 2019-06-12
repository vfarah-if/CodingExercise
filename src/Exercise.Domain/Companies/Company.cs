using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Domain.Companies
{
    public class Company
    {
        private readonly List<Employee> _employees;

        public Company(Guid? id = null)
        {
            _employees = new List<Employee>();
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public Employee GetEmployee(Guid employeeId)
        {
            return _employees.SingleOrDefault(item => item.Id == employeeId);
        }

        public Employee AddEmployee(Guid? employeeId = null)
        {
            var result = new Employee(Id, employeeId ?? Guid.NewGuid());
            _employees.Add(result);
            return result;
        }

        public bool HasEmployee(Guid employeeId)
        {
            return _employees.Any(x => x.Id == employeeId);
        }
    }
}
