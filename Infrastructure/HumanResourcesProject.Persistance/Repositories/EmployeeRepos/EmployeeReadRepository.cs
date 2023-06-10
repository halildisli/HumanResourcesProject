using HumanResourcesProject.Application.Repositories.EmployeeRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.EmployeeRepos
{
	public class EmployeeReadRepository : ReadRepository<Employee>, IEmployeeReadRepository
	{
		public EmployeeReadRepository(HumanResourcesProjectDbContext context) : base(context)
		{
		}
	}
}
