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
	public class ListOfPermissionDto
	{
		public Guid Id { get; set; }
		public PermissionType PermissionType { get; set; }
		public DateTime? StartOfPermissionDate { get; set; }
		public DateTime? EndOfPermissionDate { get; set; }
		public DateTime? RequestDate { get; set; } 
		public int? CountOfPermittedDays { get; set; }
		public PermissionStatus? ApprovalState { get; set; }
		public DateTime? ReplyDate { get; set; }
		public Guid? EmployeeId { get; set; }
		[ForeignKey("EmployeeId")]
		public virtual Employee? Employee { get; set; }
		public string? PersonId { get; set; }
		[ForeignKey("PersonId")]
		public virtual Person? Person { get; set; }
	}
}
