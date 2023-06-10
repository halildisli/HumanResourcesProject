using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Validator
{
    public class IdentityNumberValidation:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string identityNumber = value.ToString();
                if (identityNumber.Length==11)
                {
                    foreach (var item in identityNumber)
                    {
                        try
                        {
                            int number = Convert.ToInt32(item);
                        }
                        catch (Exception)
                        {

                            return new ValidationResult("The ID number must consist of numbers.");
                        }
                    }
                    return ValidationResult.Success;
                }
                return new ValidationResult("The ID number must be 11 characters.");
            }
            return new ValidationResult("You entered invalid Identity Number. Please enter a valid Identity Number.");
        }
    }
}
