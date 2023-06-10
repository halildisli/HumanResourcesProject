using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class SignInDto
    {
        [UserNameOrEmailValidation]
        public string? UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Your password should be minimum eight characters, at least one uppercase letter, one lowercase letter and one number!")]
        //[RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Your password should be minimum eight characters, at least one uppercase letter, one lowercase letter and one number!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
