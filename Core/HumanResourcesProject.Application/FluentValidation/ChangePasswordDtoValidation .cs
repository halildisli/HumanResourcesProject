using FluentValidation;
using HumanResourcesProject.Application.DTOs;

namespace HumanResourcesProject.Application.FluentValidation
{
    public class ChangePasswordDtoValidation : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidation()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("The new password field cannot be left blank..")
                .MinimumLength(8).WithMessage("The new password must be at least 8 characters long")
                .Matches(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[^\da-zA-Z]).{8,}$")
                .WithMessage("The new password must contain at least one uppercase letter, one lowercase letter, one number and one special character.");


            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword)
                .WithMessage("The new password must be the same as Verify password.");
        }

    }
}
