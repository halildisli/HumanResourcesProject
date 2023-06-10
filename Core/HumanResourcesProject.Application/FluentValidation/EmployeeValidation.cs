using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {
        public EmployeeValidation()
        {
            RuleFor(u => u.FirstName)
           .NotEmpty().WithMessage("{FirstName} field required.")
           .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("{FirstName} field can only contain uppercase and lowercase letters.")
           .Length(3, 25).WithMessage("{FirstName} cannot be less than 3 characters and more than 25 characters.");


            RuleFor(u => u.SecondName)
           .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("{SecondName} field can only contain uppercase and lowercase letters.")
           .Length(3, 25).WithMessage("{SecondName} cannot be less than 3 characters and more than 25 characters.");


            RuleFor(u => u.Surname)
           .NotEmpty().WithMessage("{Surname} field required.")
           .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("{Surname} field can only contain uppercase and lowercase letters.")
           .Length(3, 25).WithMessage("{Surname} cannot be less than 3 characters and more than 25 characters.");


            RuleFor(u => u.SecondSurname)
           .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("{SecondSurname} field can only contain uppercase and lowercase letters.")
           .Length(3, 25).WithMessage("{SecondSurname} cannot be less than 3 characters and more than 25 characters.");

            RuleFor(p => p.PlaceOfBirth)
            .NotEmpty().WithMessage("{PlaceOfBirth} field required.")
            .Matches(new Regex("^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")).WithMessage("{PlaceOfBirth} field can only contain uppercase and lowercase letters.")
            .Length(2, 50).WithMessage("{PlaceOfBirth} cannot be less than 3 characters and more than 25 characters.");

            RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("{BirthDate} field required.")
            .LessThanOrEqualTo(DateTime.Today.AddYears(-100)).WithMessage("cannot be more than 100 years ago")
            .LessThan(DateTime.Now).WithMessage("{BirthDate} field cannot be later than today's date.");

            RuleFor(x => x.StartingDate)
            .NotEmpty().WithMessage("{StartingDate} field required.")
            .LessThan(DateTime.Now).WithMessage("{StartingDate} field cannot be later than today's date.");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{Address} field required.")
            .Length(10, 300).WithMessage("{Address} field must be at least 10 characters and no more than 300 characters long.");

            RuleFor(x => x.IdentityNumber)
            .NotEmpty().WithMessage("{IdentityNumber}  field required.")
            .Length(11).WithMessage("{IdentityNumber} field must be 11 characters.")
            .Matches("^[0-9]+$").WithMessage("{IdentityNumber}  field must be numbers only.");


            RuleFor(x => x.Salary)
            .NotEmpty()
            .WithMessage("{0} field required.")
            .GreaterThanOrEqualTo(8500)
            .WithMessage("{0} must be greater than or equal to 8500.");

			RuleFor(x => x.PhoneNumber)
	.NotEmpty().WithMessage("{PropertyName} field is required.")
	.Matches(@"^0\d{10}$").WithMessage("{PropertyName} must be 11 digits and start with 0.");
		}
    }
}    

    

