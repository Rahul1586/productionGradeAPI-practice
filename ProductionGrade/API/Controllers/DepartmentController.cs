using BLL.Services;
using DLL.Models;
using DLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _departmentService.GetSingle(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveDepartment(Department dept)
        {
            return Ok(await _departmentService.Insert(dept));
        }

        [HttpPut("{id}")]        
        public async Task<IActionResult> UpdateDepartment(int id, Department dept)
        {
            return Ok(await _departmentService.Update(id, dept));
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteDepartment(int id)
        {
            return Ok(await _departmentService.Delete(id));
        }

        //public static class DepartmentStatic
        //{
        //    public static List<Department> AllDepartments { get; set; } = new List<Department>();

        //    public static Department AddDepartment(Department department)
        //    {
        //        AllDepartments.Add(department);
        //        return department;
        //    }

        //    public static List<Department> GetAllDepartments()
        //    {
        //        return AllDepartments;
        //    }

        //    public static Department GetDepartment(string code)
        //    {
        //        Department result = AllDepartments.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
        //        return result;
        //    }

        //    public static Department UpdateDepartment(string code, Department department)
        //    {
        //        Department result = null;
        //        foreach(var item in AllDepartments)
        //        {
        //            if(code == item.Code)
        //            {
        //                item.Name = department.Name;
        //                result = item;
        //            }                    
        //        }
                
        //        return result;
        //    }

        //    public static Department DeleteDepartment(string code)
        //    {
        //        var department = AllDepartments.Where(x => x.Code == code).FirstOrDefault();
        //        AllDepartments = AllDepartments.Where(x => x.Code != department.Code).ToList();
        //        return department;
        //    }

        //}
    }
}