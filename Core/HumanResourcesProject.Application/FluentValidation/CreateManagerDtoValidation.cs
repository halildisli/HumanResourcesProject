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
    public class CreateManagerDtoValidation:AbstractValidator<CreateManagerDto>
    {
        public CreateManagerDtoValidation()
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
            .GreaterThan(DateTime.Today.AddYears(-100)).WithMessage("Cannot be more than 100 years ago")
            .LessThan(DateTime.Now).WithMessage("{BirthDate} field cannot be later than today's date.")
            .LessThan(DateTime.Today.AddYears(-18)).WithMessage("This person cannot be less than 18 years old.");

            RuleFor(x => x.StartingDate)
            .NotEmpty().WithMessage("{StartingDate} field required.")
            .LessThan(DateTime.Now)
            .WithMessage("{StartingDate} field cannot be later than today's date.")
            .GreaterThan(DateTime.Today.AddYears(-20))
            .WithMessage("{StartingDate} field cannot be earlier than 2003");


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
            .GreaterThan(8499)
            .WithMessage("{0} must be greater than or equal to 8500.")
            .LessThan(250001)
            .WithMessage("{0} must be less than or equal to 250000.");

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("{0} field is required.")
            .Matches(@"^0\d{10}$").WithMessage("{0} must be 11 digits and start with 0.");
        }
    }
}
