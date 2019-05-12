using Exercise.Domain.Models;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class StudentRepositoryShould
    {
        private readonly StudentRepository _studentRepository;

        public StudentRepositoryShould()
        {
            _studentRepository = new StudentRepository(TestHelper.GetAppSettings());
        }

        [Fact]
        public void CreateARepositoryWithExpectations()
        {
            _studentRepository.Should().NotBeNull();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenAddingANullStudent()
        {
            Action action = () => _studentRepository.Add(null as Student);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenDeletingANullStudent()
        {
            Func<Task> action = async () => await _studentRepository.DeleteAsync(null as Student);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenExistingANullStudent()
        {
            Func<Task> action = async () => await _studentRepository.ExistsAsync(null as Student);

            action.Should().Throw<ArgumentException>();
        }
    }
}
