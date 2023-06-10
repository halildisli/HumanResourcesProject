using HumanResourcesProject.Application.Repositories.ExpenseRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.ExpenseRepos
{
	public class ExpenseWriteRepository : WriteRepository<Expense>, IExpenseWriteRepository
	{
		public ExpenseWriteRepository(HumanResourcesProjectDbContext context) : base(context)
		{
		}
	}
}
