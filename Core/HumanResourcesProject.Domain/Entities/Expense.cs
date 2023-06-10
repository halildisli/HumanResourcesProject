using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HumanResourcesProject.Domain.Entities
{
	public class Expense:IBaseEntity
	{
		public Guid Id { get; set; }
		public decimal AmountOfExpense { get; set; } //gider tutarı
		public DateTime DateOfExpense { get; set; }  //harcama tarihi
		public string? Explanation { get; set; }
		public ExpenseType ExpenseType { get; set; }
		public Currency Currency { get; set; } = Currency.TL;
		[Display(Name = "Approval Status")]
		public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
		public DateTime? DateOfResponse { get; set; } //cevap tarihi
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public string? FilePath { get; set; }
		public Guid? EmployeeId { get; set; }
		[ForeignKey("EmployeeId")]
		public virtual Employee? Employee { get; set; }

        public string? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person? Person { get; set; }
    }
}
