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
    public interface IStudentRepository
    {
        Task<Student> Insert(Student Student);
        Task<List<Student>> GetAll();
        Task<Student> GetSingle(int id);
        Task<Student> Delete(int id);
        Task<Student> Update(int id, Student dept);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetSingle(int id)
        {
            Student Student = await _context.Students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
            return Student;
        }

        public async Task<Student> Insert(Student Student)
        {
            await _context.Students.AddAsync(Student);
            await _context.SaveChangesAsync();
            return Student;
        }

        public async Task<Student> Delete(int id)
        {
            Student Student = await _context.Students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
            return Student;
        }

        public async Task<Student> Update(int id, Student dept)
        {
            Student Student = await _context.Students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
            Student.Name = dept.Name;
            _context.Students.Update(Student);
            await _context.SaveChangesAsync();
            return Student;
        }
    }
}
