using Exercise.Domain.Models;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MongoDB.Driver;
using Xunit;

namespace Exercise.Domain.Tests.UnitTests
{
    public class StudentRepositoryShould
    {
        private readonly Fixture _fixture;
        private readonly StudentRepository _studentRepository;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMongoDatabase> _mongoDatabaseMock;
        private Mock<IMongoCollection<Student>> _mongoCollectionMock;
        
        public StudentRepositoryShould()
        {
            _fixture = new Fixture();
            SetupMongoDbDependencyMocks();
            _studentRepository = new StudentRepository(_unitOfWorkMock.Object);
        }

        private void SetupMongoDbDependencyMocks()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mongoCollectionMock = new Mock<IMongoCollection<Student>>();
            _mongoDatabaseMock = new Mock<IMongoDatabase>();
            _mongoDatabaseMock
                .Setup(x => x.GetCollection<Student>("Student", null))
                .Returns(_mongoCollectionMock.Object);
            _unitOfWorkMock
                .SetupGet<IMongoDatabase>(x => x.Database)
                .Returns(_mongoDatabaseMock.Object);
        }

        [Fact]
        public void CreateARepositoryWithExpectations()
        {
            _studentRepository.Should().NotBeNull();
            _mongoDatabaseMock
                .Verify(x => x.GetCollection<Student>("Student", null), Times.Once);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenAddingANullStudent()
        {
            Action action = () => _studentRepository.Add(null as Student);

            action.Should().Throw<ArgumentException>();
        }

        /// <remarks>
        /// This is an example of how I could mock every call verifying inputs and setting up outputs
        /// but as I was able to achieve everything I needed through the acceptance tests,
        /// working with the real database was more useful for developing this, but 
        /// below is an example of how I could verify values going into properties/methods.
        /// Personally I found the acceptance test an easier and more reliable way to verify that
        /// what I was wrapping would actually work with MongoDb, and also I would probably do this for the base
        /// repository type only, doing this once and only testing what I would add in the StudentRepository explicitly
        /// </remarks>>
        [Fact]
        public void InvokeInsertOneWithTheStudentDataIfAddingAStudent()
        {
            var student = _fixture.Create<Student>();
            _studentRepository.Add(student);

            _mongoCollectionMock.Verify(x => 
                x.InsertOne(student, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
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
