using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.DTOs
{
    public class ListOfExpenseDto
    {
        public Guid Id { get; set; }
        public decimal AmountOfExpense { get; set; }
        public DateTime DateOfExpense { get; set; }
        public DateTime? DateOfResponse { get; set; }
        public string? Explanation { get; set; }
        public string? FilePath { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public Currency Currency { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
