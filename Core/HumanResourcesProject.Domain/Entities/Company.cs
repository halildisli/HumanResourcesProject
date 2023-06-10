using HumanResourcesProject.Domain.Enums;
using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResourcesProject.Domain.Entities
{
    public class Company : IBaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? MersisNo { get; set; }
        public string? TaxNo { get; set; }
        public string? TaxAdministration { get; set; }
        public string? Logo { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime FoundedDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public Status? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        //Navigation Property
        public virtual List<Person> Persons { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
