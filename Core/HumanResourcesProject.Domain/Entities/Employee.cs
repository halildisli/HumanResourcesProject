using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HumanResourcesProject.Domain.Validator;

namespace HumanResourcesProject.Domain.Entities
{
	public class Employee:IBaseEntity
	{ 
        public Guid Id { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }  
        public string Surname { get; set; }  
        public string? SecondSurname { get; set; }      
        public decimal Salary { get; set; }
        public Status Status { get; set; } = Status.Passive;     
        public string PlaceOfBirth { get; set; }
     
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [JoinDateValidation(ErrorMessage = "Join Date cannot be greater than current date")]
        public DateTime BirthDate { get; set; }     
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Address { get; set; }
      
        public string IdentityNumber { get; set; }
        public string? ImagePath { get; set; }
        public Gender Gender { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? JobId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        //Navigation Properties
        public virtual List<Advance>? Advances { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        public virtual List<Permission> Permissions { get; set; }
    }
}
