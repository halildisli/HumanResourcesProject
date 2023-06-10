using HumanResourcesProject.Domain.Enums;
using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class CreateManagerDto
    {
        public Guid Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public decimal? Salary { get; set; }
        public Status Status { get; set; } = Status.Active;
        public string PlaceOfBirth { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JoinDateValidation(ErrorMessage = "Join Date cannot be greater than current date")]
        public DateTime BirthDate { get; set; }

        [JoinDateValidation]
        public DateTime? StartingDate { get; set; }
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public string? ImagePath { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? JobId { get; set; }
    }
}
