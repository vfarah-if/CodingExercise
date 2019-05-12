using System;
using System.Threading.Tasks;
using Exercise.Domain.Models;
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

        [Fact]
        public void ThrowArgumentNullExceptionWhenAddingANullStudent()
        {
            Action action = () => _studentRepository.Add(null);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenDeletingANullStudent()
        {
            Func<Task> action = async () => await _studentRepository.DeleteAsync(null as Student);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenDeletingANullStudentId()
        {
            Func<Task> action = async () => await _studentRepository.DeleteAsync(null as string);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenExistingANullStudent()
        {
            Func<Task> action = async () => await _studentRepository.ExistsAsync(null as Student);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenExistingANullStudentId()
        {
            Func<Task> action = async () => await _studentRepository.ExistsAsync(null as string);

            action.Should().Throw<ArgumentException>();
        }
    }
}
