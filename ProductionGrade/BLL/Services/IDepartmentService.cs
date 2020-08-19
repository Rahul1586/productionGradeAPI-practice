using BLL.Request;
using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> Insert(DepartmentInsertRequestViewModel request);
        Task<List<Department>> GetAll();
        Task<Department> GetSingle(int id);
        Task<Department> Delete(int id);
        Task<Department> Update(int id, DepartmentUpdateRequestViewModel dept);
        Task<bool> IsCodeExist(string code, int? id = 0);
        Task<bool> IsNameExist(string name, int? id = 0);
        //Task<bool> IsCodeExistForEdit(int id, string code);
        //Task<bool> IsNameExistForEdit(int id, string name);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }        

        public async Task<List<Department>> GetAll()
        {
            return await _departmentRepository.GetAll();
        }

        public async Task<Department> GetSingle(int id)
        {
            var department = await _departmentRepository.GetSingle(id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }
            return department;
        }

        public async Task<Department> Insert(DepartmentInsertRequestViewModel request)
        {
            var department = new Department
            {
                Code = request.Code,
                Name = request.Name
            };
            return await _departmentRepository.Insert(department);
        }

        public async Task<Department> Update(int id, DepartmentUpdateRequestViewModel dept)
        {
            var department = await _departmentRepository.GetSingle(id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            department.Code = dept.Code;
            department.Name = dept.Name;

            if (await _departmentRepository.Update(department))
            {
                return department;
            }
            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Department> Delete(int id)
        {
            var department = await _departmentRepository.GetSingle(id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            if (await _departmentRepository.Delete(department))
            {
                return department;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<bool> IsCodeExist(string code, int? id = 0)
        {
            if (id != 0)
            {
                bool isDeptPresent = await _departmentRepository.FindByCodeForEdit(code, id);

                if (!isDeptPresent)
                {
                    return true;
                }

                return false;
            }
            else
            {
                Department department = await _departmentRepository.FindByCode(code);

                if (department == null)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task<bool> IsNameExist(string name, int? id = 0)
        {
            if (id != 0)
            {
                bool isDeptPresent = await _departmentRepository.FindByNameForEdit(name, id);

                if (!isDeptPresent)
                {
                    return true;
                }

                return false;
            }
            else
            {
                Department department = await _departmentRepository.FindByName(name);

                if (department == null)
                {
                    return true;
                }

                return false;
            }
            
        }

        //public async Task<bool> IsCodeExistForEdit(int id, string code)
        //{
        //    Department department = await _departmentRepository.FindByCode(code);

        //    if (department == null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public async Task<bool> IsNameExistForEdit(int id, string name)
        //{
        //    Department department = await _departmentRepository.FindByName(name);

        //    if (department == null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
