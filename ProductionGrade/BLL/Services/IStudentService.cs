using DLL.Models;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IStudentRepository _StudentRepository;

        public StudentService(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public async Task<Student> Delete(int id)
        {
            return await _StudentRepository.Delete(id);
        }

        public async Task<List<Student>> GetAll()
        {
            return await _StudentRepository.GetAll();
        }

        public async Task<Student> GetSingle(int id)
        {
            return await _StudentRepository.GetSingle(id);
        }

        public async Task<Student> Insert(Student Student)
        {
            return await _StudentRepository.Insert(Student);
        }

        public async Task<Student> Update(int id, Student dept)
        {
            return await _StudentRepository.Update(id, dept);
        }
    }
}
