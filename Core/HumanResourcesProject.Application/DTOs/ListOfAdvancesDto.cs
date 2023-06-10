using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class ListOfAdvancesDto
    {
        public Guid Id { get; set; }
        public decimal AmountOfDemand { get; set; }
        public DateTime DateOfDemand { get; set; }
        public DateTime? DateOfResponse { get; set; }
        public string? Explanation { get; set; }
        public AdvanceType AdvanceType { get; set; }
        public Currency Currency { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
