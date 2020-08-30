using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
       
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
