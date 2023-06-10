using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
	public class CompanyValidation : AbstractValidator<Company>
	{
		public CompanyValidation()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("{Name} field is required.")
				.Length(5, 100).WithMessage("{Name} must be between 5 and 100 characters.")
				.WithName("Company Name")
				.OverridePropertyName("Name");

			RuleFor(x => x.Phone)
			.NotEmpty().WithMessage("{PropertyName} field is required.")
			.Matches(@"^0\d{10}$").WithMessage("Phone number must be 11 characters and start with '0' (zero).")
			.WithName("Job Phone Number")
			.OverridePropertyName("Phone");

			RuleFor(x => x.Address)
			.NotEmpty().WithMessage("{Address} field is required.")
			.MinimumLength(20).WithMessage("{Address} cannot be less than 20 characters.")
			.MaximumLength(300).WithMessage("{Address} cannot be more than 300 characters.")
			.WithName("Company Address")
			.OverridePropertyName("Address");

			RuleFor(x => x.Email)
			.NotEmpty().WithMessage("{Email} field is required.")
			.EmailAddress().WithMessage("{Email} is invalid. Please enter a valid {Email}.")
			.WithName("Company Email")
			.OverridePropertyName("Email");

			RuleFor(x => x.FoundedDate)
			  .NotEmpty().WithMessage("{FoundedDate} field is required.")
			  .LessThan(DateTime.Now).WithMessage("{StartingDate} field cannot be later than today's date.")
			  .WithName("Company Founded Date")
			  .OverridePropertyName("FoundedDate");


		}


	}
}
