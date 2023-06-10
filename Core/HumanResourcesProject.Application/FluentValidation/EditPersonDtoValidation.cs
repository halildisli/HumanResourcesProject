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
    public class EditPersonDtoValidation : AbstractValidator<EditPersonDto>
    {
        
        Regex regex = new Regex(@"^05[0-9]{9,9}");
        public EditPersonDtoValidation()
        {
            RuleFor(x => x.Address)
          .NotEmpty().WithMessage("{Address} field required.")
          .Length(10, 300).WithMessage("{Address} field must be at least 10 characters and no more than 300 characters long.");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("{PhoneNumber} field is required.")
           .Matches(regex).WithMessage("Phone number must be 11 characters and start with '0' (zero).");
        }
    }
}
