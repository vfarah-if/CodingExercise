using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CoreBDD;
using Exercise.Domain.Models;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class StudentRepositoryShould: StudentRepositoryFeature
    {
        private Student _student;
        private StudentRepository _studentRepository;
        private readonly Fixture _autoFixture;
        private IEnumerable<Student> _students;

        public StudentRepositoryShould()
        {
           _autoFixture = new Fixture();
        }

        [Scenario("Should create, delete and check if students exist")]
        public void CreateAStudent()
        {
            Given("a student", () =>
            {
                _student = _autoFixture.Build<Student>()
                    .Without(x => x.Id)
                    .Create();
                _student.Id.ShouldBe(null);
            });
            When("when adding the student", () =>
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
            Then("student should be removed", async () =>
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
                _studentRepository.Add(_students);
            });
            Then("should be able to retrieve all the added students", async () =>
            {
                var actual = await _studentRepository.ListAsync();
                actual.Data.Should().NotBeNullOrEmpty();
                actual.CurrentPage.Should().Be(1);
                actual.LastPage.Should().Be(1);
            });
            Then("students should be removed", () =>
            {
                _students.ToList().ForEach(async item =>
                {
                    await _studentRepository.DeleteAsync(item);
                    var exists = await _studentRepository.ExistsAsync(item);
                    exists.Should().BeFalse();
                });
            });
        }

        [Scenario("Should update an existing student")]
        public void UpdateAStudent()
        {
            string previousFirstname = null;
            string previousSurname = null;
            int previousAge = default(int);
            Given("an existing student", () =>
            {
                _student = _autoFixture.Build<Student>()
                    .Without(x => x.Id)
                    .Create();
                _studentRepository = new StudentRepository(TestHelper.GetAppSettings());
                _student = _studentRepository.Add(_student);
                previousFirstname = _student.Firstname;
                previousSurname = _student.Surname;
                previousAge = _student.Age;
            });
            When("updating the students firstname, surname and age", async () =>
            {
                _student.Firstname = _autoFixture.Create<string>();
                _student.Surname = _autoFixture.Create<string>();
                _student.Age = _autoFixture.Create<int>();
                _student = await _studentRepository.UpdateAsync(_student);
            });
            Then("student should exist by the new values", async () =>
            {
                var exists = await _studentRepository.ExistsAsync(x => 
                    x.Id == _student.Id && 
                    x.Firstname == _student.Firstname &&
                    x.Surname == _student.Surname &&
                    x.Age == _student.Age);
                exists.Should().BeTrue();
            });
            And("the previous student data should not exist", async () =>
            {
                var exists = await _studentRepository.ExistsAsync(x =>
                    x.Id == _student.Id &&
                    x.Firstname == previousFirstname &&
                    x.Surname == previousSurname &&
                    x.Age == previousAge);
                exists.Should().BeFalse();
            });           
            Then("student should be removed", async () =>
            {
                await _studentRepository.DeleteAsync(_student);
                var exists = await _studentRepository.ExistsAsync(_student);
                exists.Should().BeFalse();
            });
        }
    }
}
