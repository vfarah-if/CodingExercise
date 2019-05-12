using Exercise.Domain.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Exercise.Domain
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        //        public StudentRepository(IConfiguration config)
        public StudentRepository()
        {
            //            var client = new MongoClient(config.GetConnectionString("StudentsDb"));
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("StudentsDb");
            //            database.CreateCollection("Students");
            _students = database.GetCollection<Student>("Students");

        }

        public Student Add(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            _students.InsertOne(student);
            return student;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            return await DeleteAsync(student.Id);
        }

        public async Task<bool> DeleteAsync(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                throw new ArgumentException("message", nameof(studentId));
            }
            var result = await _students.DeleteOneAsync(x => x.Id == studentId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> ExistsAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            return await ExistsAsync(student.Id);
        }

        public async Task<bool> ExistsAsync(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                throw new ArgumentException("message", nameof(studentId));
            }

            var result = await _students.CountDocumentsAsync(x => x.Id == studentId);
            return result > 0;
        }
    }
}
