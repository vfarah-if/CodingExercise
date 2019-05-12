using System;
using AutoFixture;
using CoreBDD;
using Exercise.Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class StudentRepositoryShould: StudentRepositoryFeature
    {
        private Student _student;
        private StudentRepository _studentRepository;
        private Fixture _autoFixture;

        public StudentRepositoryShould()
        {
           _autoFixture = new Fixture();
        }

        [Scenario("Should create, delete and check if students exist")]
        public void CreateStudents()
        {
            Given("a student", () =>
            {
                _student = _autoFixture.Build<Student>()
                    .Without(x => x.Id)
                    .Create();
                _student.Id.ShouldBe(null);
            });
            When("when creating the student", () =>
            {
//                ConfigurationBuilder builder = new ConfigurationBuilder();
//                builder.Sources.AddJsonFile("appsettings.json").;
//                builder.Properties.Add("ConnectionStrings", new {StudentsDb = @"mongodb://localhost:27017"});
//                _studentRepository = new StudentRepository(builder.Build());
                _studentRepository = new StudentRepository();
                _student =  _studentRepository.Add(_student);
            });
            Then("student should be persisted", async () =>
            {
                _student.Id.Should().NotBeNullOrEmpty();
            });
            And("student should exist", async () =>
            {
                var exists = await _studentRepository.ExistsAsync(_student);
                exists.Should().BeTrue();
            });
            And("student should then be removed", async () =>
            {
                await _studentRepository.DeleteAsync(_student);
                var exists = await _studentRepository.ExistsAsync(_student);
                exists.Should().BeFalse();
            });
        }
    }
}
