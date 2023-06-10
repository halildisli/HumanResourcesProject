using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResourcesProject.Application.DTOs
{
    public class EditPersonDto
    {
        public Guid Id { get; set; }
        //[Display(Name = "Address")]
        //[Required(ErrorMessage = "{0} field is required.")]
        //[MinLength(20, ErrorMessage = "{0} cannot be less than 20 characters."), MaxLength(300, ErrorMessage = "{0} cannot be more than 300 characters.")]
        public string Address { get; set; }
        //[Display(Name = "Phone Number")]
        //[Required(ErrorMessage = "{0} field is required.")]
        //[Phone(ErrorMessage = "Invalid {0}. Please enter a valid {0}")]
        //[PhoneNumberValidation]
        public string PhoneNumber { get; set; }
        public string? ImagePath { get; set; }
    }
}
