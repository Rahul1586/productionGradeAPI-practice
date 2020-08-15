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
    public interface IDepartmentRepository
    {
        Task<Department> Insert(Department department);
        Task<List<Department>> GetAll();
        Task<Department> GetSingle(int id);
        Task<Department> Delete(int id);
        Task<Department> Update(int id, Department dept);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();            
        }

        public async Task<Department> GetSingle(int id)
        {
            Department department = await _context.Departments.Where(x => x.DepartmentId == id).FirstOrDefaultAsync();
            return department;
        }

        public async Task<Department> Insert(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Delete(int id)
        {
            Department department = await _context.Departments.Where(x => x.DepartmentId == id).FirstOrDefaultAsync();
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Update(int id, Department dept)
        {
            Department department = await _context.Departments.Where(x => x.DepartmentId == id).FirstOrDefaultAsync();
            department.Name = dept.Name;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }
}
