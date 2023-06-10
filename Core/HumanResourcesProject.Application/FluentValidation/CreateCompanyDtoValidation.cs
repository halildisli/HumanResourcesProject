using FluentValidation;
using HumanResourcesProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
    public class CreateCompanyDtoValidation: AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidation()
        {
            RuleFor(u => u.Name)
          .NotEmpty().WithMessage("Company name is required.")
          .Length(3, 50).WithMessage("Company name cannot be less than 3 characters and more than 50 characters.");


            RuleFor(u => u.Title)
          .NotEmpty().WithMessage("Comppany title is required.")
           .Length(3, 50).WithMessage("Comppany title cannot be less than 3 characters and more than 50 characters.");


            RuleFor(u => u.MersisNo)
            .NotEmpty().WithMessage("Mersis No is required.")
            .Matches(@"^0\d{15}$").WithMessage("Mersis No must be only digits and start with 0.");


            RuleFor(u => u.TaxNo)
            .NotEmpty().WithMessage("Tax No is required.")
            .Matches(@"^\d{10}$").WithMessage("Tax No must be only digits.");

            RuleFor(p => p.TaxAdministration)
            .NotEmpty().WithMessage("Tax Administartion is required.")
            .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("Tax Administartion can only contain uppercase and lowercase letters.")
            .Length(3, 50).WithMessage("Tax Administration cannot be less than 3 characters and more than 25 characters.");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^0\d{10}$").WithMessage("Phone must be 11 digits and start with 0.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .Length(10, 300).WithMessage("Address must be at least 10 characters and no more than 300 characters long.");



            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be correct format. example: info@example.com ");

            RuleFor(x => x.FoundedDate)
            .NotEmpty().WithMessage("Founded date is required.")
            .LessThan(DateTime.Now)
            .WithMessage("Founded date cannot be later than today's date.");



            RuleFor(x => x.ContractStartDate)
            .NotEmpty().WithMessage("Contract start date is required.")
            .LessThan(DateTime.Now)
            .WithMessage("Contract start date cannot be later than today's date.");

        }
    }
}
