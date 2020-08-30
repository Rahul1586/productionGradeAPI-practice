using BLL.Request;
using BLL.Services;
using DLL.Repositories;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ViewModels;

namespace BLL
{
    public static class BLLDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
            AllFluentValidationDependency(services);
        }

        public static void AllFluentValidationDependency(IServiceCollection services)
        {
            services.AddTransient<IValidator<DepartmentRequestViewModel>, DepartmentInsertRequestViewModelValidator>();
        }

    }
}
