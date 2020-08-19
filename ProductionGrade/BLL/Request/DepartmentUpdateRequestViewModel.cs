using BLL.Services;
using DLL.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class DepartmentUpdateRequestViewModel
    {
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentUpdateRequestViewModelValidator : AbstractValidator<DepartmentUpdateRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentUpdateRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            DepartmentUpdateRequestViewModel model = new DepartmentUpdateRequestViewModel();
            RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(2)
                .MaximumLength(100);
            RuleFor(x => x).MustAsync((x, model) => CodeExists(x)).WithMessage("Code Already Exists");
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(100);
            RuleFor(x => x).MustAsync((x, model) => NameExists(x)).WithMessage("Name Already Exists");
        }

        private async Task<bool> NameExists(DepartmentUpdateRequestViewModel x)
        {
            if (string.IsNullOrEmpty(x.Name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var dept = await requiredService.GetSingle(x.DepartmentId);
            return await requiredService.IsNameExist(x.Name, x.DepartmentId);
        }

        private async Task<bool> CodeExists(DepartmentUpdateRequestViewModel x)
        {
            if (string.IsNullOrEmpty(x.Code))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var dept = await requiredService.GetSingle(x.DepartmentId);
            return await requiredService.IsCodeExist(x.Code, x.DepartmentId);
        }

        //private async Task<bool> NameExists(string name, CancellationToken arg2)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return true;
        //    }

        //    var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
        //    return await requiredService.IsNameExist(name);
        //}
    }
}
