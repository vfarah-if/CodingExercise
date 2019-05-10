using System;
using CoreBDD;
using Exercise.Domain.Models;
using FluentAssertions;

namespace Exercise.Domain.Tests.AcceptanceTests
{
    public class StudentRepositoryShould: StudentRepositoryFeature
    {
        private Student _student;
        private StudentRepository _studentRepository;

        public StudentRepositoryShould()
        {
            //drop the database or clear the data on every run to reset the data 
        }

        [Scenario("Should create students")]
        public void CreateUsers()
        {
            Given("a student", () =>
            {
                _student = new Student
                {
                    Age = 10,
                    Firstname = "Vincent",
                    Surname = "Farah",
                    Salutation = "Mr"
                };
            });
            When("when creating the student", () =>
            {
                _studentRepository = new StudentRepository();
                _student = _studentRepository.Create(_student);
            });
            Then("student should be persisted", () =>
            {
                _student.Id.Should().NotBeNullOrEmpty();
            });
        }
    }
}
