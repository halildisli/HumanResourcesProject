using FluentValidation;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.FluentValidation
{
	public class ExpenseValidation:AbstractValidator<Expense>
	{
		public ExpenseValidation()
		{
			When(x => x.ExpenseType == ExpenseType.FoodAndDrink, () =>
			{
				RuleFor(x => x.AmountOfExpense)
				.NotEmpty()
				.WithName("AmountOfExpense")
				.WithMessage("Please enter an amount")
				.GreaterThanOrEqualTo(x => 10)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be less than 10 TL")
				.LessThanOrEqualTo(x => 1000)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be greater than 1000 TL");
			});
			When(x => x.ExpenseType == ExpenseType.Travel, () =>
			{
				RuleFor(x => x.AmountOfExpense)
				.NotEmpty()
				.WithName("AmountOfExpense")
				.WithMessage("Please enter an amount")
				.GreaterThanOrEqualTo(x => 1)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be less than 1 TL")
				.LessThanOrEqualTo(x => 3000)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be greater than 3000 TL");
			});
			When(x => x.ExpenseType == ExpenseType.Accomodation, () =>
			{
				RuleFor(x => x.AmountOfExpense)
				.NotEmpty()
				.WithName("AmountOfExpense")
				.WithMessage("Please enter an amount")
				.GreaterThanOrEqualTo(x => 100)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be less than 100 TL")
				.LessThanOrEqualTo(x => 1000)
				.WithName("AmountOfExpense")
				.WithMessage("Your amount of expense can not be greater than 1000 TL");
			});


			RuleFor(x => x.DateOfExpense)
			.NotEmpty().WithMessage("{DateOfExpense} field required.")
			.GreaterThan(DateTime.Now)
			.WithMessage("{DateOfExpense} field cannot be earlier than 1 year")
			.LessThan(DateTime.Today.AddYears(-1))
			.WithMessage("{DateOfExpense}  field cannot be later than today's date");
		}
	}
}
