using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
    public class PhoneNumberValidation:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string phoneNumber = Convert.ToString(value);
                if (phoneNumber.Length==11)
                {
                    if (phoneNumber[0]=='0')
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Phone number must be start '0'(zero).");
                    }
                }
                else
                {
                    return new ValidationResult("Phone number must be 11 characters.");
                }
            }
            return new ValidationResult("You entered an invalid value. Please enter a value in phone format.");
        }
    }
}
