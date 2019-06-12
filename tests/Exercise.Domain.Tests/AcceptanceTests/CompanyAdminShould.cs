using System;
using System.Linq;
using CoreBDD;
using Exercise.Domain.Companies;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class CompanyAdminShould: EmployeeAndBookingPolicyFeature
    {
        private CompanyService _companyService;
        private Guid _employeeId;
        private Guid _companyId;
        private CompanyRepository _companyRepository;

        public CompanyAdminShould()
        {
            _companyRepository = new CompanyRepository();
            _companyRepository.Add();
            _companyService = new CompanyService(_companyRepository);
        }

        [Scenario("Associate Employees with a Company ...")]
        public void HaveTheAbilityToAssociateEmployeesWithACompany()
        {
            Given("an employee and a company", () =>
            {
                var company = _companyRepository.List().First();
                _companyId = company.Id;
                _employeeId = Guid.NewGuid();
            });
            When("associating the employee with a company", () =>
            {
                _companyService.AddEmployee(_companyId, _employeeId);
            });
            Then("the employee should be linked to the company", () =>
            {
                var company = _companyRepository.GetBy(_companyId);
                var employee = company.GetEmployee(_employeeId);
                employee.Should().NotBeNull();
                employee.CompanyId.Should().NotBeEmpty();
                employee.CompanyId.Should().Be(_companyId);
            });
        }
    }
}
