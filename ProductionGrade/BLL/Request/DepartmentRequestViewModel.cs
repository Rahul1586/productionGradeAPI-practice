using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class DepartmentRequestViewModel
    {
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentInsertRequestViewModelValidator : AbstractValidator<DepartmentRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;


            When(x => x.DepartmentId == 0, () => {
                RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(2)
                .MaximumLength(10).MustAsync(CodeExists).WithMessage("Code Already Exists");
                RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(100).MustAsync(NameExists).WithMessage("Name Already Exists");
            }).Otherwise(() => {
                RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(2)
                .MaximumLength(100);
                RuleFor(x => x).MustAsync((x, model) => CodeExists(x)).WithMessage("Code Already Exists");
                RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(100);
                RuleFor(x => x).MustAsync((x, model) => NameExists(x)).WithMessage("Name Already Exists");
            });          
        }

       
        private async Task<bool> CodeExists(string code, CancellationToken arg2)
        {
            if(string.IsNullOrEmpty(code))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.IsCodeExist(code);
        }

        private async Task<bool> NameExists(string name, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.IsNameExist(name);
        }

        private async Task<bool> NameExists(DepartmentRequestViewModel x)
        {
            if (string.IsNullOrEmpty(x.Name))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var dept = await requiredService.GetSingle(x.DepartmentId);
            return await requiredService.IsNameExist(x.Name, x.DepartmentId);
        }

        private async Task<bool> CodeExists(DepartmentRequestViewModel x)
        {
            if (string.IsNullOrEmpty(x.Code))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            var dept = await requiredService.GetSingle(x.DepartmentId);
            return await requiredService.IsCodeExist(x.Code, x.DepartmentId);
        }
    }
}
