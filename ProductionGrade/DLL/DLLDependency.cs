using DLL.DBContext;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DLL
{
    public static class DLLDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            //Database Dependency
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Repository Dependency
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
