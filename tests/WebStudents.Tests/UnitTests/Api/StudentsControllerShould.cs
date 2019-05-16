using System;
using System.Threading.Tasks;
using AutoFixture;
using Exercise.Domain;
using Exercise.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStudents.Controllers.Api;
using WebStudents.Models;
using Xunit;

namespace WebStudents.Tests.UnitTests.Api
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

        // ListAsync

        [Fact]
        public async Task GetPaginatedListFromStudentsRepository()
        {
            await _studentsController.List().ConfigureAwait(false);

            _studentsRepositoryMock.Verify(x => x.ListAsync(null, 1, 100), Times.Once);
        }

        [Fact]
        public async Task ReturnOkResultWithPaginatedData()
        {
            var response = _fixture.Create<PagedResult<Student, string>>();
            _studentsRepositoryMock
                .Setup(x => x.ListAsync(null, 1, 100))
                .ReturnsAsync(response);

            var actual = await _studentsController.List().ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult) actual.Result).Value.Should().Be(response);
        }

        // GetByAsync

        [Fact]
        public async Task ReturnBadRequestWithArgumentNullExceptionDataWhenGetAsyncIsAssignedANullId()
        {
            var actual = await _studentsController.Get(null).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = (BadRequestObjectResult) actual.Result;
            badRequest.Value.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task CallGetAsyncRepositoryWithId()
        {
            var id = _fixture.Create<string>();

            await _studentsController.Get(id).ConfigureAwait(false);

            _studentsRepositoryMock.Verify(x => x.GetByAsync(id), Times.Once);
        }

        [Fact]
        public async Task CallGetByAsyncAndReturnNotFoundWithIdWhenRepositoryGetByAsyncReturnsNullData()
        {
            var id = _fixture.Create<string>();
            _studentsRepositoryMock
                .Setup(x => x.GetByAsync(id))
                .ReturnsAsync(null as Student);

            var actual = await _studentsController.Get(id).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<NotFoundObjectResult>();
            ((NotFoundObjectResult) actual.Result).Value.Should().Be(id);
        }

        [Fact]
        public async Task CallGetByAsyncAndReturnOkWithStudentDataWhenStudentFound()
        {
            var id = _fixture.Create<string>();
            var studentResponse = _fixture.Create<Student>();
            _studentsRepositoryMock
                .Setup(x => x.GetByAsync(id))
                .ReturnsAsync(studentResponse);

            var actual = await _studentsController.Get(id).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult) actual.Result).Value.Should().Be(studentResponse);
        }

        // Post

        [Fact]
        public void PostAndReturnBadRequestWithArgumentNullExceptionWhenAssignedANullStudent()
        {
            var actual = _studentsController.Post(null);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult) actual.Result).Value.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void PostAndReturnBadRequestWithSerializableErrorIssuesWhenTheModelStateIsInvalid()
        {
            SetupModelStateError();
            var student = _fixture.Create<StudentModel>();

            var actual = _studentsController.Post(student);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult) actual.Result).Value.Should().BeOfType<SerializableError>();
            ((SerializableError) ((BadRequestObjectResult) actual.Result).Value).Count.Should().Be(1);
        }

        [Fact]
        public void PostAndAddStudentAndVerifyStudentIsAdded()
        {
            var student = GenerateNewStudent();
            SetupAddResponse(student);

            _studentsController.Post(student);

            _studentsRepositoryMock.Verify(x => x.Add(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void PostAndReturnCreatedResponseWithNewStudentIdANdTheGetLocationOfTheNewId()
        {
            var student = GenerateNewStudent();
            SetupAddResponse(student);

            var actual = _studentsController.Post(student);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<CreatedAtActionResult>();
            ((CreatedAtActionResult) actual.Result).Value.Should().BeOfType<Student>();
            ((CreatedAtActionResult) actual.Result).ActionName.Should().Be("GetByAsync");
        }

        // Put

        [Fact]
        public async Task PutAndReturnBadRequestWithArgumentExceptionWhenAssignedANullId()
        {
            var actual = await _studentsController.Put(null, GenerateNewStudent()).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)actual.Result).Value.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public async Task PutAndReturnBadRequestWithArgumentNullExceptionWhenAssignedANullStudent()
        {
            var actual = await _studentsController.Put(_fixture.Create<string>(), null).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)actual.Result).Value.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task PutAndAndReturnBadRequestWithSerializableErrorIssuesWhenTheModelStateIsInvalid()
        {
            // simulate error that comes from the model binder validation
            SetupModelStateError();
            var student = _fixture.Create<StudentModel>();
            var id = _fixture.Create<string>();
            var actual = await _studentsController.Put(id, student).ConfigureAwait(false);
        
            actual.Should().NotBeNull();
            actual.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)actual.Result).Value.Should().BeOfType<SerializableError>();
            ((SerializableError)((BadRequestObjectResult)actual.Result).Value).Count.Should().Be(1);
        }

        [Fact]
        public async Task PutReturnNotFoundResponseIfNoStudentFound()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(null as Student);
            SetupUpdateResponse(student);

            var actual = await _studentsController.Put(id, student).ConfigureAwait(false);

            actual.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task PutAndUpdateStudentAndVerifyStudentIsUpdated()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(StudentModel.MapFrom(student));
            SetupUpdateResponse(student);

            await _studentsController.Put(id, student).ConfigureAwait(false);

            _studentsRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task PutAndUpdateStudentAndReturnOkResponse()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(StudentModel.MapFrom(student));
            SetupUpdateResponse(student);

            var actual = await _studentsController.Put(id, student).ConfigureAwait(false);

            actual.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)actual.Result).Value.Should().BeOfType<Student>();
        }

        // Delete

        [Fact]
        public async Task DeleteAndReturnBadRequestWithArgumentExceptionWhenAssignedANullId()
        {
            var actual = await _studentsController.Delete(null).ConfigureAwait(false);

            actual.Should().NotBeNull();
            actual.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)actual).Value.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public async Task DeleteReturnNotFoundResponseIfNoStudentFound()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(null as Student);
            SetupUpdateResponse(student);

            var actual = await _studentsController.Delete(id).ConfigureAwait(false);

            actual.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteAndVerifyStudentIsDeleted()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(StudentModel.MapFrom(student));
            
            await _studentsController.Delete(id).ConfigureAwait(false);

            _studentsRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAndReturnNoContentResult()
        {
            var id = _fixture.Create<string>();
            var student = GenerateNewStudent();
            _studentsRepositoryMock.Setup(x => x.GetByAsync(id)).ReturnsAsync(StudentModel.MapFrom(student));

            var actual = await _studentsController.Delete(id).ConfigureAwait(false);

            actual.Should().BeOfType<NoContentResult>();
        }

        private void SetupAddResponse(StudentModel student)
        {
            var postResponse = StudentModel.MapFrom(student);
            postResponse.Id = _fixture.Create<string>();
            _studentsRepositoryMock.Setup(x => x.Add(It.IsAny<Student>())).Returns(postResponse);
        }

        private void SetupUpdateResponse(StudentModel student)
        {
            var putResponse = StudentModel.MapFrom(student);
            _studentsRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Student>())).ReturnsAsync(putResponse);
        }

        private StudentModel GenerateNewStudent()
        {
            return _fixture.Build<StudentModel>()
                .Without(x => x.Id)
                .Create();
        }

        private void SetupModelStateError()
        {
            var key = _fixture.Create<string>();
            var error = _fixture.Create<string>();
            _studentsController.ModelState.AddModelError(key, error);
        }
    }
}

