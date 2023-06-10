using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
    public class BirthDateValidation:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                double overEigthteen = (DateTime.Today - (DateTime)value).TotalDays/365;
                if (overEigthteen>=18)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Cannot be younger than 18 years old.");
                }
            }
            return new ValidationResult("You entered an invalid value. Please enter a value in date format.");
        }
    }
}
