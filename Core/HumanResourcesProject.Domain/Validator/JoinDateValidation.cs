using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
    public class JoinDateValidation:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime startingDate = Convert.ToDateTime(value);
            if (startingDate<=DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The start(founded) date cannot be later than today. Please enter a valid date.");
            }
        }
    }
}
