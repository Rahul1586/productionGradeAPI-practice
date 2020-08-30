using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> Insert(Student Student);
        Task<List<Student>> GetAll();
        Task<Student> GetSingle(int id);
        Task<Student> Delete(int id);
        Task<Student> Update(int id, Student dept);
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> Delete(int id)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);

            if (student == null)
            {
                throw new ApplicationValidationException("Student Not Found");
            }

            _unitOfWork.StudentRepository.Delete(student);

            if (await _unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return student;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<List<Student>> GetAll()
        {
            return await _unitOfWork.StudentRepository.GetList();
        }

        public async Task<Student> GetSingle(int id)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);

            if (student == null)
            {
                throw new ApplicationValidationException("Student Not Found");
            }
            return student;
        }

        public async Task<Student> Insert(Student request)
        {
            var student = new Student
            {
                Email = request.Email,
                Name = request.Name
            };
            await _unitOfWork.StudentRepository.CreateAsync(student);

            if (await _unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return student;
            }

            throw new ApplicationValidationException("Something went wrong");
        }

        public async Task<Student> Update(int id, Student stud)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);

            if (student == null)
            {
                throw new ApplicationValidationException("Student Not Found");
            }

            student.Email = stud.Email;
            student.Name = stud.Name;

            _unitOfWork.StudentRepository.Update(student);

            if (await _unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return student;
            }
            throw new ApplicationValidationException("Something went wrong");
        }
    }
}
