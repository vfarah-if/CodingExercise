using AutoFixture;
using Exercise.Domain;
using Exercise.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WebStudents.Controllers.Api;
using Xunit;

namespace WebStudents.Tests
{
    public class StudentsControllerShould
    {
        private readonly Mock<IRepository<Student>> _studentsRepositoryMock;
        private readonly StudentsController _studentsController;
        private readonly Fixture _fixture;

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
        public async Task GetPaginatedListFromStudentsRepository()
        {
            await _studentsController.ListAsync();

            _studentsRepositoryMock.Verify(x => x.ListAsync(null, 1, 100), Times.Once);
        }

        [Fact]
        public async Task ReturnOkResultWithPaginatedData()
        {
            var response = _fixture.Create<PagedResult<Student, string>>();
            _studentsRepositoryMock
                .Setup(x => x.ListAsync(null, 1, 100))
                .ReturnsAsync(response);

            var actual = await _studentsController.ListAsync();

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)actual.Result).Value.Should().Be(response);
        }

        [Fact]
        public async Task ReturnBadRequestWithArgumentNullExceptionDataWhenGetAsyncIsAssignedANullId()
        {
            var actual = await _studentsController.GetByAsync(null);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = (BadRequestObjectResult)actual.Result;
            badRequest.Value.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task CallGetAsyncRepositoryWithId()
        {
            var id = _fixture.Create<string>();

            await _studentsController.GetByAsync(id);

            _studentsRepositoryMock.Verify(x => x.GetByAsync(id), Times.Once);
        }

        [Fact]
        public async Task CallGetByAsyncAndReturnNotFoundWithIdWhenRepositoryGetByAsyncReturnsNullData()
        {
            var id = _fixture.Create<string>();
            _studentsRepositoryMock
                .Setup(x => x.GetByAsync(id))
                .ReturnsAsync( null as Student);

            var actual = await _studentsController.GetByAsync(id);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<NotFoundObjectResult>();
            ((NotFoundObjectResult)actual.Result).Value.Should().Be(id);
        }

        [Fact]
        public async Task CallGetByAsyncAndReturnOkWithStudentDataWhenStudentFound()
        {
            var id = _fixture.Create<string>();
            var studentResponse = _fixture.Create<Student>();
            _studentsRepositoryMock
                .Setup(x => x.GetByAsync(id))
                .ReturnsAsync(studentResponse);

            var actual = await _studentsController.GetByAsync(id);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)actual.Result).Value.Should().Be(studentResponse);
        }
    }
}
