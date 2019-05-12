using Exercise.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;

namespace Exercise.Domain
{
    public class StudentRepository : RepositoryBase<Student, string>
    {
        public StudentRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public StudentRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
