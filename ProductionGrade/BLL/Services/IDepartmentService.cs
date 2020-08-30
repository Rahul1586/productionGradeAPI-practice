using BLL.Request;
using DLL.Models;
using DLL.Repositories;
using DLL.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Exceptions;
using ViewModels.Request;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> Insert(DepartmentRequestViewModel request);
        Task<PagedResponse<List<Department>>> GetAll(DepartmentPaginationViewModel model);
        Task<Department> GetSingle(int id);
        Task<Department> Delete(int id);
        Task<Department> Update(int id, DepartmentRequestViewModel dept);
        Task<bool> IsCodeExist(string code, int? id = 0);
        Task<bool> IsNameExist(string name, int? id = 0);       
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _unitOfWork.DepartmentRepository.GetList();
        }

        public async Task<PagedResponse<List<Department>>> GetAll(DepartmentPaginationViewModel model)
        {            
            return await _unitOfWork.DepartmentRepository.GetPagedDepartments(model);
        }

        public async Task<Department> GetSingle(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);

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
            await _unitOfWork.DepartmentRepository.CreateAsync(department);

            if(await _unitOfWork.DepartmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Department> Update(int id, DepartmentRequestViewModel dept)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            department.Code = dept.Code;
            department.Name = dept.Name;

            _unitOfWork.DepartmentRepository.Update(department);

            if (await _unitOfWork.DepartmentRepository.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Department> Delete(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                throw new ApplicationValidationException("Department Not Found");
            }

            _unitOfWork.DepartmentRepository.Delete(department);

            if (await _unitOfWork.DepartmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<bool> IsCodeExist(string code, int? id = 0)
        {
            if (id != 0)
            {
                var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code && x.DepartmentId != id);
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
                Department department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);

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
                var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == name && x.DepartmentId != id);

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
                Department department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == name);

                if (department == null)
                {
                    return true;
                }

                return false;
            }
            
        }       
    }
}
