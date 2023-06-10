using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
    public class JobValidation:AbstractValidator<Job>
    {
        public JobValidation()
        {
            RuleFor(j => j.Name)
           .NotEmpty().WithMessage("{PropertyName} field is required.")
           .Length(5, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

            RuleFor(j => j.Description)
          .NotEmpty().WithMessage("{PropertyName} field is required.")
          .Length(5, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");




        }
    }
}
