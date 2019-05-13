using System;
using Exercise.Domain.Models;
using Microsoft.Extensions.Configuration;

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
