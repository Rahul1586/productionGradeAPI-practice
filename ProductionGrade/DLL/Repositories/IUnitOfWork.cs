using DLL.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private DepartmentRepository _departmentRepository;
        private StudentRepository _studentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IDepartmentRepository DepartmentRepository =>
            _departmentRepository ??= new DepartmentRepository(_context);
        public IStudentRepository StudentRepository =>
            _studentRepository ??= new StudentRepository(_context);
    }
}
