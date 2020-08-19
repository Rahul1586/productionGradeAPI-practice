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
        Task<bool> Delete(Department dept);
        Task<bool> Update(Department dept);
        Task<Department> FindByCode(string code, int? id = 0);
        Task<Department> FindByName(string name, int? id = 0);
        Task<bool> FindByCodeForEdit(string code, int? id = 0);
        Task<bool> FindByNameForEdit(string name, int? id = 0);
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

        public async Task<bool> Delete(Department dept)
        {
            _context.Departments.Remove(dept);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Department dept)
        {            
            _context.Departments.Update(dept);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Department> FindByCode(string code, int? id = 0)
        {
            return await _context.Departments.Where(x => x.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Department> FindByName(string name, int? id = 0)
        {
            return await _context.Departments.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> FindByCodeForEdit(string code, int? id = 0)
        {
            var deptCount = await _context.Departments.Where(x => x.Code == code && x.DepartmentId != id).CountAsync();
            return deptCount > 0 ? true : false;
        }

        public async Task<bool> FindByNameForEdit(string name, int? id = 0)
        {
            var deptCount = await _context.Departments.Where(x => x.Name == name && x.DepartmentId != id).CountAsync();
            return deptCount > 0 ? true : false;
        }
    }
}
