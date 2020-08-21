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
        Task<Department> Insert(DepartmentRequestViewModel request);
        Task<List<Department>> GetAll();
        Task<Department> GetSingle(int id);
        Task<Department> Delete(int id);
        Task<Department> Update(int id, DepartmentRequestViewModel dept);
        Task<bool> IsCodeExist(string code, int? id = 0);
        Task<bool> IsNameExist(string name, int? id = 0);       
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
            return await _departmentRepository.GetList();
        }

        public async Task<Department> GetSingle(int id)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }
            return department;
        }

        public async Task<Department> Insert(DepartmentRequestViewModel request)
        {
            var department = new Department
            {
                Code = request.Code,
                Name = request.Name
            };
            await _departmentRepository.CreateAsync(department);

            if(await _departmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Department> Update(int id, DepartmentRequestViewModel dept)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            department.Code = dept.Code;
            department.Name = dept.Name;

            _departmentRepository.Update(department);

            if (await _departmentRepository.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Department> Delete(int id)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            _departmentRepository.Delete(department);

            if (await _departmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<bool> IsCodeExist(string code, int? id = 0)
        {
            if (id != 0)
            {
                var department = await _departmentRepository.FindSingleAsync(x => x.Code == code && x.DepartmentId != id);
                //if(dept.DepartmentId > 0)
                //{
                //    return false;
                //}
                //return true;
                if (department == null)
                {
                    return true;
                }

                return false;
            }
            else
            {
                Department department = await _departmentRepository.FindSingleAsync(x => x.Code == code);

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
                var department = await _departmentRepository.FindSingleAsync(x => x.Name == name && x.DepartmentId != id);

                //if (!isDeptPresent)
                //{
                //    return true;
                //}
                if (department == null)
                {
                    return true;
                }

                return false;
            }
            else
            {
                Department department = await _departmentRepository.FindSingleAsync(x => x.Name == name);

                if (department == null)
                {
                    return true;
                }

                return false;
            }
            
        }       
    }
}
