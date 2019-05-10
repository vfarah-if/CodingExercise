using System;
using System.Collections.Generic;
using System.Text;
using Exercise.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Exercise.Domain
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _students;

        // TODO: Wire this with configuration if time permits
        //public StudentRepository(IConfiguration config)
        public StudentRepository()
        {
            //var client = new MongoClient(config.GetConnectionString("StudentsDb"));
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("StudentsDb");
            _students = database.GetCollection<Student>("Students");
        }

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }
    }
}
