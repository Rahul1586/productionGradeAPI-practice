using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace API.Controllers
{    
    public class DepartmentController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DepartmentStatic.GetAllDepartments());
        }

        [HttpGet("{code}")]
        public IActionResult GetSingle(string code)
        {
            return Ok(DepartmentStatic.GetDepartment(code));
        }

        [HttpPost]
        public IActionResult SaveDepartment(Department dept)
        {
            return Ok(DepartmentStatic.AddDepartment(dept));
        }

        [HttpPut("{code}")]        
        public IActionResult UpdateDepartment(string code, Department dept)
        {
            return Ok(DepartmentStatic.UpdateDepartment(code, dept));
        }

        [HttpDelete("{code}")]
        public IActionResult DeleteDepartment(string code)
        {
            return Ok(DepartmentStatic.DeleteDepartment(code));
        }

        public static class DepartmentStatic
        {
            public static List<Department> AllDepartments { get; set; } = new List<Department>();

            public static Department AddDepartment(Department department)
            {
                AllDepartments.Add(department);
                return department;
            }

            public static List<Department> GetAllDepartments()
            {
                return AllDepartments;
            }

            public static Department GetDepartment(string code)
            {
                Department result = AllDepartments.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
                return result;
            }

            public static Department UpdateDepartment(string code, Department department)
            {
                Department result = null;
                foreach(var item in AllDepartments)
                {
                    if(code == item.Code)
                    {
                        item.Name = department.Name;
                        result = item;
                    }                    
                }
                
                return result;
            }

            public static Department DeleteDepartment(string code)
            {
                var department = AllDepartments.Where(x => x.Code == code).FirstOrDefault();
                AllDepartments = AllDepartments.Where(x => x.Code != department.Code).ToList();
                return department;
            }

        }
    }
}