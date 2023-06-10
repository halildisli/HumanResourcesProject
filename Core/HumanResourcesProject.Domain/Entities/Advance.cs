using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Entities
{
    public class Advance:IBaseEntity
    {
        public Guid Id { get; set; }
        public decimal AmountOfDemand { get; set; }
        public DateTime DateOfDemand { get; set; } = DateTime.Now;
        public string? Explanation { get; set; }
        public AdvanceType AdvanceType { get; set; }
        public Currency Currency { get; set; } = Currency.TL;
        //public int NumberOfDemand { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
        public DateTime? DateOfResponse { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
