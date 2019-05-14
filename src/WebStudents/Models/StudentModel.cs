using Exercise.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebStudents.Models
{
    public class StudentModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Salutation { get; set; }
        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        public static Student MapFrom(StudentModel studentModel)
        {
            if (studentModel == null)
            {
                throw new ArgumentNullException(nameof(studentModel));
            }

            return Student.Create(studentModel.Id, studentModel.Salutation, studentModel.Age, studentModel.Firstname, studentModel.Surname);
        }

        public static StudentModel MapFrom(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            return StudentModel.Create(student.Id, student.Salutation, student.Age, student.Firstname, student.Surname);
        }

        public static StudentModel Create(string studentId, string studentSalutation, int studentAge, string studentFirstname, string studentSurname)
        {
            return new StudentModel
            {
                Id = studentId,
                Age = studentAge,
                Surname = studentSurname,
                Firstname = studentFirstname,
                Salutation = studentSalutation
            };
        }
    }
}
