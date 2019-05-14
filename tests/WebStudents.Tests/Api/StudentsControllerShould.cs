using System;
using Exercise.Domain;
using Exercise.Domain.Models;
using FluentAssertions;
using Moq;
using WebStudents.Controllers.Api;
using Xunit;

namespace WebStudents.Tests
{
    public class StudentsControllerShould
    {
        private readonly Mock<IRepository<Student>> _studentsRepositoryMock;
        private readonly StudentsController _studentsController;

        public StudentsControllerShould()
        {
            _studentsRepositoryMock = new Mock<IRepository<Student>>();
            _studentsController = new StudentsController(_studentsRepositoryMock.Object);
        }

        [Fact]
        public void CreateWithExpectations()
        {
            _studentsController.Should().NotBeNull();
        }
    }
}
