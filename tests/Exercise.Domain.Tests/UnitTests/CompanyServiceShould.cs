using System;
using Exercise.Domain.Companies;
using FluentAssertions;
using Moq;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class CompanyServiceShould
    {
        private readonly Mock<CompanyRepository> _companyRepositoryMock;
        private readonly CompanyService _companyService;

        public CompanyServiceShould()
        {
            _companyRepositoryMock = new Mock<CompanyRepository>();
            _companyService = new CompanyService(_companyRepositoryMock.Object);
        }

        [Fact]
        public void AddAnEmployeeToTheCompanyRepository()
        {
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            _companyService.AddEmployee(companyId, employeeId);

            _companyRepositoryMock.Verify(x => x.AddEmployee(companyId, employeeId), Times.Once);
        }
    }
}
