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
        private EmployeeRepository _employeeRepository;

        public CompanyAdminShould()
        {
            _companyRepository = new CompanyRepository();
            _employeeRepository = new EmployeeRepository();
            _companyService = new CompanyService(_companyRepository, _employeeRepository);
        }

        [Scenario("Associate Employees with a Company ...")]
        public void ValidateAnyExpectationsThroughTheScenario()
        {
            Given("a valid employee and a company", () =>
            {
                var company = _companyRepository.List().First();
                _companyId = company.Id;
                var employee = _employeeRepository.List().First();
                _employeeId = employee.Id;
            });
            When("associating the employee with a company", () =>
            {
                _companyService.AddEmployee(_companyId, _employeeId);
            });
            Then("the employee should now be associated with this company", () =>
            {
                var employee = _employeeRepository.GetBy(_employeeId);
                employee.Should().NotBeNull();
                employee.CompanyId.Should().NotBeEmpty();
                employee.CompanyId.Should().Be(_companyId);
            });
        }
    }
}
