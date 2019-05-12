﻿using Exercise.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;

namespace Exercise.Domain
{
    public class StudentRepository
    {
       private readonly IMongoCollection<Student> _students;

        public StudentRepository(IConfiguration configuration)
            : this(UnitOfWork.Create(configuration), configuration.GetSection("StudentsCollectionName").Value)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
        }

        public StudentRepository(IUnitOfWork unitOfWork, string collectionName)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (collectionName == null)
            {
                throw new ArgumentNullException(nameof(collectionName));
            }
           
            _students = unitOfWork.Database.GetCollection<Student>(collectionName);
        }

        /// <remarks>
        /// The advantage of a synchronous add is the InsertOne generates an Id without needing
        /// a get to retrieve the data
        /// </remarks>
        public Student Add(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            _students.InsertOne(student);
            return student;
        }

        public async Task AddAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            await _students.InsertOneAsync(student);
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

        public async Task<IReadOnlyCollection<Student>> ListAsync(int page = 1, int pageSize = 100)
        {
            if (page < 0)
            {
                page = 1;
            }

            double totalDocuments = _students.CountDocuments(FilterDefinition<Student>.Empty);
            var maxPageCount = Math.Ceiling(totalDocuments/pageSize);
            if (page > maxPageCount)
            {
                page = (int) maxPageCount;
            }
            return await  _students
                .AsQueryable()
                .Skip(page - 1)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
