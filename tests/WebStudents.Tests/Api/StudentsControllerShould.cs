using AutoFixture;
using Exercise.Domain;
using Exercise.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStudents.Controllers.Api;
using Xunit;

namespace WebStudents.Tests
{
    public class StudentsControllerShould
    {
        private readonly Mock<IRepository<Student>> _studentsRepositoryMock;
        private readonly StudentsController _studentsController;
        private Fixture _fixture;

        public StudentsControllerShould()
        {
            _fixture = new Fixture();
            _studentsRepositoryMock = new Mock<IRepository<Student>>();
            _studentsController = new StudentsController(_studentsRepositoryMock.Object);
        }

        [Fact]
        public void CreateWithExpectations()
        {
            _studentsController.Should().NotBeNull();
        }

        [Fact]
        public async void GetPaginatedListFromStudentsRepository()
        {
            await _studentsController.GetListAsync();

            _studentsRepositoryMock.Verify(x => x.ListAsync(null, 1, 100), Times.Once);
        }

        [Fact]
        public async void ReturnOkResultWithPaginatedData()
        {
            var response = _fixture.Create<PagedResult<Student, string>>();
            _studentsRepositoryMock
                .Setup(x => x.ListAsync(null, 1, 100))
                .ReturnsAsync(response);

            var actual = await _studentsController.GetListAsync();

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)actual.Result).Value.Should().Be(response);
        }
    }
}
