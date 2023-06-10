using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class SignUpDto
    {
        [MaxLength(30, ErrorMessage = "Username cannot be greater than 30 characters!")]
        [MinLength(5, ErrorMessage = "Username cannot be greater than 30 characters!")]
        [Required]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Your password should be minimum eight characters, at least one uppercase letter, one lowercase letter and one number!")]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Your password should be minimum eight characters, at least one uppercase letter, one lowercase letter and one number!")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password fields do not match!")]
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
