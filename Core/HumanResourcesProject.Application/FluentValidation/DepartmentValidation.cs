using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
    public class DepartmentValidation:AbstractValidator<Department>
    {
        public DepartmentValidation()
        {
            RuleFor(x => x.Name)
          .NotEmpty().WithMessage("{Name} field is required.")
          .Length(5, 100).WithMessage("{Name} must be between {5} and {100} characters.");

            RuleFor(x => x.Description)
            .NotEmpty().WithMessage("{PropertyName} field is required.")
            .Length(5, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");
        }
    

    }
}
