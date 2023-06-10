using HumanResourcesProject.Domain.Enums;
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
    public class UpdateEmployeeDto
    {
        //private const string RegularExpression = @"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$";
        public Guid Id { get; set; }
        public string? PhoneNumber { get; set; }

        //[Display(Name = "First Name")]
        //[Required(ErrorMessage = "{0} field required.")]
        //[RegularExpression(RegularExpression, ErrorMessage = "{0} field can only contain uppercase and lowercase letters.")]
        //[MinLength(3, ErrorMessage = "{0} cannot be less than 2 characters."), MaxLength(25, ErrorMessage = "{0} cannot be more than 25 characters.")]
        public string FirstName { get; set; }
        //[Display(Name = "Second Name")]
        //[RegularExpression(RegularExpression, ErrorMessage = "{0} field can only contain uppercase and lowercase letters.")]
        //[MinLength(3, ErrorMessage = "{0} 3 karakterden az olamaz."), MaxLength(25, ErrorMessage = "{0} 25 karakterden fazla olamaz.")]
        public string? SecondName { get; set; }
        //[Display(Name = "Surname")]
        //[Required(ErrorMessage = "{0} field required.")]
        //[RegularExpression(RegularExpression, ErrorMessage = "{0} field can only contain uppercase and lowercase letters.")]
        //[MinLength(2, ErrorMessage = "{0} cannot be less than 2 characters."), MaxLength(25, ErrorMessage = "{0} cannot be more than 25 characters.")]
        public string Surname { get; set; }
        //[Display(Name = "Second Surname")]
        //[RegularExpression(RegularExpression, ErrorMessage = "{0} field can only contain uppercase and lowercase letters.")]
        //[MinLength(5, ErrorMessage = "{0} cannot be less than 2 characters."), MaxLength(25, ErrorMessage = "{0} cannot be more than 25 characters.")]
        public string? SecondSurname { get; set; }
        //[Required(ErrorMessage = "{0} field required.")]
        //[Range(8500, double.MaxValue)]
        public decimal Salary { get; set; }
        //[Display(Name = "Place Of Birth")]
        //[Required(ErrorMessage = "{0} field required.")]
        //[RegularExpression(RegularExpression, ErrorMessage = "{0} field can only contain uppercase and lowercase letters. Please enter only province or region.")]
        public string PlaceOfBirth { get; set; }
        //[Display(Name = "Birth Date")]
        //[Required(ErrorMessage = "{0} field required.")]
        //[BirthDateValidation]
        public DateTime BirthDate { get; set; }
        //[Display(Name = "Birth Date")]
        //[Required(ErrorMessage = "{0} field required.")]
        //[JoinDateValidation]
        public DateTime StartingDate { get; set; }
        public string Address { get; set; }
        //[Display(Name = "Identity Number")]
        //[MaxLength(11, ErrorMessage = "{0} must 11 characters."), MinLength(11, ErrorMessage = "{0} must 11 characters.")]
        //[IdentityNumberValidation]
        public string IdentityNumber { get; set; }
        public string? ImagePath { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? JobId { get; set; }
    }
}
