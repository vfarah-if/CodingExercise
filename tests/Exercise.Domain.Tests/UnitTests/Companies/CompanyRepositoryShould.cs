using System;
using System.Linq;
using Exercise.Domain.Companies;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests.Companies
{
    public class CompanyRepositoryShould
    {
        private readonly CompanyRepository _companyRepository;
        private readonly Guid _employeeId;
        private readonly Guid _companyId;

        public CompanyRepositoryShould()
        {
            _companyRepository = new CompanyRepository();
            _companyId = Guid.NewGuid();
            _employeeId = Guid.NewGuid();
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
            _companyRepository.AddEmployee(_companyId, _employeeId);

            var company = _companyRepository.GetBy(_companyId);
            company.Should().NotBeNull();
            var employee = company.GetEmployee(_employeeId);
            employee.Should().NotBeNull();
            employee.Id.Should().Be(_employeeId);
        }

        [Fact]
        public void AddOnlyOneUniqueEmployeeToACompany()
        {
            _companyRepository.AddEmployee(_companyId, _employeeId);
            _companyRepository.AddEmployee(_companyId, _employeeId);

            var company = _companyRepository.GetBy(_companyId);
            var employee = company.GetEmployee(_employeeId);
            employee.Should().NotBeNull();
        }

        [Fact]
        public void ListAnyCompaniesAdded()
        {
            _companyRepository.List().Any().Should().BeFalse();

            _companyRepository.AddEmployee(_companyId, _employeeId);

            _companyRepository.List().Any().Should().BeTrue();
        }
    }
}
