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
        private CompanyService _companyService;
        private CompanyRepository _companyRepository;
        private Guid _employeeId;
        private Guid _companyId;
        private BookingPolicyService _bookingPolicyService;


        [Scenario("Associate Employees with a Company ...")]
        public void HaveTheAbilityToAssociateEmployeesWithACompany()
        {
            _companyRepository = new CompanyRepository();
            _companyRepository.Add();
            _companyService = new CompanyService(_companyRepository);
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

        [Scenario("Employee should be allowed to book a room if the employee policy allows this")]
        public void AllowEmployeeToBookARoomWhenEmployeePolicyAllows()
        {
            var roomType = Guid.Empty;

            Given("an employee booking policy, employee and a room type", () =>
            {
                roomType = Guid.NewGuid();
                _employeeId = Guid.NewGuid();
                _bookingPolicyService = new BookingPolicyService();
            });
            When("an employee policies exist for that room type", () =>
            {
                _bookingPolicyService.SetEmployeePolicy(_employeeId, new[] { roomType });
            });
            Then("the employee booking should be allowed", () =>
            {
                _bookingPolicyService.IsBookingAllowed(_employeeId, roomType).Should().BeTrue();
            });
        }
        // TODO: Should be able to book any room if the company policy allows this
    }
}
