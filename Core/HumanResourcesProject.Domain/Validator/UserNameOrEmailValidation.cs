using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
    public class UserNameOrEmailValidation:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string passwordOrEmail = Convert.ToString(value);
            if ((passwordOrEmail.Contains("@")) && ((passwordOrEmail.EndsWith(".com")) || (passwordOrEmail.EndsWith(".co")) || (passwordOrEmail.EndsWith(".com.tr"))))
            {
                Regex regex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
                if (regex.IsMatch(passwordOrEmail))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("This email is invalid. Please enter a valid email or username!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
