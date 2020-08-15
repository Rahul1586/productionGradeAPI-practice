using DLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class StudentController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(StudentStatic.GetAllStudents());
        }

        [HttpGet("{email}")]
        public IActionResult GetSingle(string email)
        {
            return Ok(StudentStatic.GetStudent(email));
        }

        [HttpPost]
        public IActionResult SaveStudent(Student dept)
        {
            return Ok(StudentStatic.AddStudent(dept));
        }

        [HttpPut("{email}")]
        public IActionResult UpdateStudent(string email, Student dept)
        {
            return Ok(StudentStatic.UpdateStudent(email, dept));
        }

        [HttpDelete("{email}")]
        public IActionResult DeleteStudent(string email)
        {
            return Ok(StudentStatic.DeleteStudent(email));
        }

        public static class StudentStatic
        {
            public static List<Student> AllStudents { get; set; } = new List<Student>();

            public static Student AddStudent(Student Student)
            {
                AllStudents.Add(Student);
                return Student;
            }

            public static List<Student> GetAllStudents()
            {
                return AllStudents;
            }

            public static Student GetStudent(string email)
            {
                Student result = AllStudents.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
                return result;
            }

            public static Student UpdateStudent(string email, Student Student)
            {
                Student result = null;
                foreach (var item in AllStudents)
                {
                    if (email == item.Email)
                    {
                        item.Name = Student.Name;
                        result = item;
                    }
                }

                return result;
            }

            public static Student DeleteStudent(string email)
            {
                var Student = AllStudents.Where(x => x.Email == email).FirstOrDefault();
                AllStudents = AllStudents.Where(x => x.Email != Student.Email).ToList();
                return Student;
            }

        }
    }
}
