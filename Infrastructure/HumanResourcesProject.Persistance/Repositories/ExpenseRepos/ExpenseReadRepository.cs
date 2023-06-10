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
	public class ExpenseReadRepository : ReadRepository<Expense>, IExpenseReadRepository
	{
		public ExpenseReadRepository(HumanResourcesProjectDbContext context) : base(context)
		{

		}
	}
}
