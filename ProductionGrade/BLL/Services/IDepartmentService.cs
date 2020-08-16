using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> Insert(Department department);
        Task<List<Department>> GetAll();
        Task<Department> GetSingle(int id);
        Task<Department> Delete(int id);
        Task<Department> Update(int id, Department dept);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Delete(int id)
        {
            return await _departmentRepository.Delete(id);
        }

        public async Task<List<Department>> GetAll()
        {
            return await _departmentRepository.GetAll();
        }

        public async Task<Department> GetSingle(int id)
        {
            return await _departmentRepository.GetSingle(id);
        }

        public async Task<Department> Insert(Department department)
        {
            return await _departmentRepository.Insert(department);
        }

        public async Task<Department> Update(int id, Department dept)
        {
            return await _departmentRepository.Update(id, dept);
        }
    }
}
