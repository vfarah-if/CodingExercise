using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
