using System;
using Exercise.Domain.Companies;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Companies
{
    public class CompanyRepositoryShould
    {
        private CompanyRepository _companyRepository;

        public CompanyRepositoryShould()
        {
            _companyRepository = new CompanyRepository();
        }

        [Fact]
        public void AddANewCompany()
        {
            var actual = _companyRepository.Add();

            actual.Should().NotBeNull();
            actual.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void AddAnEmployeeToACompany()
        {
            Guid companyId = Guid.NewGuid();
            Guid employeeId = Guid.NewGuid();

            _companyRepository.AddEmployee(companyId, employeeId);

            var company = _companyRepository.GetBy(companyId);
            company.Should().NotBeNull();
            var employee = company.GetEmployee(employeeId);
            employee.Should().NotBeNull();
            employee.Id.Should().Be(employeeId);
        }
    }
}
