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
    public class StudentController : MainApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _studentService.GetSingle(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveStudent(Student dept)
        {
            return Ok(await _studentService.Insert(dept));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student dept)
        {
            return Ok(await _studentService.Update(id, dept));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return Ok(await _studentService.Delete(id));
        }

        //public static class StudentStatic
        //{
        //    public static List<Student> AllStudents { get; set; } = new List<Student>();

        //    public static Student AddStudent(Student Student)
        //    {
        //        AllStudents.Add(Student);
        //        return Student;
        //    }

        //    public static List<Student> GetAllStudents()
        //    {
        //        return AllStudents;
        //    }

        //    public static Student GetStudent(string email)
        //    {
        //        Student result = AllStudents.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        //        return result;
        //    }

        //    public static Student UpdateStudent(string email, Student Student)
        //    {
        //        Student result = null;
        //        foreach (var item in AllStudents)
        //        {
        //            if (email == item.Email)
        //            {
        //                item.Name = Student.Name;
        //                result = item;
        //            }
        //        }

        //        return result;
        //    }

        //    public static Student DeleteStudent(string email)
        //    {
        //        var Student = AllStudents.Where(x => x.Email == email).FirstOrDefault();
        //        AllStudents = AllStudents.Where(x => x.Email != Student.Email).ToList();
        //        return Student;
        //    }

        //}
    }
}
