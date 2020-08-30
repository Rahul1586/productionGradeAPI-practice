using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using DLL.Wrapper;
using ViewModels.Request;

namespace DLL.Repositories
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        Task<PagedResponse<List<Department>>> GetPagedDepartments(DepartmentPaginationViewModel model);
    }

    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<Department>>> GetPagedDepartments(DepartmentPaginationViewModel model)
        {
            IQueryable<Department> pagedData = _context.Departments;

            if (!string.IsNullOrEmpty(model.Name))
            {
                pagedData = pagedData.Where(x => x.Name.Contains(model.Name));
            }

            if (!string.IsNullOrWhiteSpace(model.OrderingField))
            {
                pagedData = pagedData
                    .OrderBy($"{model.OrderingField} {(model.AscendingOrder ? "ascending" : "descending")}");
            }

            pagedData = pagedData
               .Skip((model.PageNumber - 1) * model.PageSize)
               .Take(model.PageSize);

            var totalRecords = await pagedData.CountAsync();

            return new PagedResponse<List<Department>>(await pagedData.ToListAsync(), model.PageNumber, model.PageSize, totalRecords);
        }
    }
}
