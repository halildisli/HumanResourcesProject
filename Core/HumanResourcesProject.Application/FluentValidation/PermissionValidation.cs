using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
	public class PermissionValidation : AbstractValidator<Permission>
	{
		public PermissionValidation()
		{
			RuleFor(p => p.EndOfPermissionDate).NotNull().GreaterThan(p => p.StartOfPermissionDate).WithMessage("StartofPermissionDate cannot be later than EndofPermissionDate");

			RuleFor(x => x.StartOfPermissionDate)
			.NotEmpty().WithMessage("{0} field required.")
			.GreaterThan(DateTime.Now).WithMessage("{0} field cannot be before than today's date.");
			
		}
	}
}

