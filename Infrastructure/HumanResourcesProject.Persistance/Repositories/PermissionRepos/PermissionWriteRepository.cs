using HumanResourcesProject.Application.Repositories.PermissionRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.PermissionRepos
{
	public class PermissionWriteRepository : WriteRepository<Permission>, IPermissionWriteRepository
	{
		public PermissionWriteRepository(HumanResourcesProjectDbContext context) : base(context)
		{
		}
	}
}
