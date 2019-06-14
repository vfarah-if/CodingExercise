using CoreBDD;
using Exercise.Domain.Bookings;
using Exercise.Domain.Companies;
using FluentAssertions;
using System;
using System.Linq;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class CompanyAdminShould : EmployeeAndBookingPolicyFeature
    {
        private readonly CompanyService _companyService;
        private readonly CompanyRepository _companyRepository;
        private Guid _employeeId;
        private Guid _companyId;
        private BookingPolicyService _bookingPolicyService;

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

        [Scenario("Employee should be allowed to book any room if there are no company or employee policies")]
        public void AllowAnyEmployeeToBookARoomIfThereAreNoCompanyOrPolicies()
        {
            Given("an employee booking policy", () =>
            {
                _bookingPolicyService = new BookingPolicyService();
            });
            When("no company or employee policies exist", () =>
            {
            });
            Then("the employee booking should be allowed", () =>
            {
                Guid roomType = Guid.NewGuid();
                _bookingPolicyService.IsBookingAllowed(_employeeId, roomType).Should().BeTrue();
            });
        }

        // TODO: Should be able to book any room if the employee policy allows this
        // TODO: Should be able to book any room if the company policy allows this
    }
}
