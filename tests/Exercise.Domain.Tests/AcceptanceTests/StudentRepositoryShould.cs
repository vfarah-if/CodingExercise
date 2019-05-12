using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private IEnumerable<Student> _students;

        public StudentRepositoryShould()
        {
           _autoFixture = new Fixture();
        }

        [Scenario("Should create, delete and check if students exist")]
        public void CreateAStudent()
        {
            Given("several students", () =>
            {
                _student = _autoFixture.Build<Student>()
                    .Without(x => x.Id)
                    .Create();
                _student.Id.ShouldBe(null);
            });
            When("when adding the students", () =>
            {
                _studentRepository = new StudentRepository(TestHelper.GetAppSettings());
                _student = _studentRepository.Add(_student);
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

        [Scenario("Should list many students")]
        public void CreateAndGetSeveralStudents()
        {
            Given("many students", () =>
            {
                _students = _autoFixture.Build<Student>()
                    .Without(x => x.Id)
                    .CreateMany();
                _students.Should().NotBeEmpty();
            });
            When("when adding the students", () =>
            {
                _studentRepository = new StudentRepository(TestHelper.GetAppSettings());
                _students.ToList().ForEach(student => _studentRepository.Add(student));
            });
            Then("should be able to retrieve all the added students", async () =>
            {
                var actual = await _studentRepository.ListAsync();
                actual.Should().NotBeNullOrEmpty();
            });
            And("students should then be removed", () =>
            {
                _students.ToList().ForEach(async item =>
                {
                    await _studentRepository.DeleteAsync(item);
                    var exists = await _studentRepository.ExistsAsync(item);
                    exists.Should().BeFalse();
                });
            });
        }
    }
}
