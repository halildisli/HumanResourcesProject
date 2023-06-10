using HumanResourcesProject.Domain.Enums;
using HumanResourcesProject.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Domain.Entities
{
	public class Permission:IBaseEntity
	{
		public Guid Id { get; set; }
		public PermissionType PermissionType { get; set; }
		public DateTime? StartOfPermissionDate { get; set; }
		public DateTime? EndOfPermissionDate { get; set; }
		public DateTime? RequestDate { get; set; } = DateTime.Now;
		//[YearlyPermissonValidator]
		public int? CountOfPermittedDays { get; set; }
		public PermissionStatus? ApprovalState { get; set; } = PermissionStatus.Pending;
		public DateTime? ReplyDate { get; set; }
		//Navigation

		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }

		public Guid? EmployeeId { get; set; }
		[ForeignKey("EmployeeId")]
		public virtual Employee? Employee { get; set; }
		public string? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person? Person { get; set; }
	}
}
