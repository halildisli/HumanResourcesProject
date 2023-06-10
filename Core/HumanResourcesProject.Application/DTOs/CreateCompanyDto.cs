using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class CreateCompanyDto
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
    }
}
