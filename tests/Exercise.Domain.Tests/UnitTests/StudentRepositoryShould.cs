using System;
using FluentAssertions;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class StudentRepositoryShould
    {
        private readonly StudentRepository _studentRepository;

        public StudentRepositoryShould()
        {
            _studentRepository = new StudentRepository();
        }

        [Fact]
        public void CreateARepositoryWithExpectations()
        {
            _studentRepository.Should().NotBeNull();
        }
    }
}
