using HumanResourcesProject.Application;
using HumanResourcesProject.Application.Repositories.PermissionRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.PermissionRepos
{
	public class PermissionReadRepository : ReadRepository<Permission>, IPermissionReadRepository
	{
		HumanResourcesProjectDbContext _context;
		public PermissionReadRepository(HumanResourcesProjectDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<TimeSpan> GetCountOfUsedDaysByEmployeeIdAsync(Guid? employeeId)
		{
			if (employeeId == null)
			{
				throw new ArgumentNullException(nameof(employeeId));
			}

			var permissions = await _context.Permissions.Where(p => p.EmployeeId == employeeId).ToListAsync();
			//double countOfUsedDays = permissions.Sum(p => p.CountOfPermittedDays.Value.TotalDays);
			double countOfUsedDays = permissions.Sum(p => p.CountOfPermittedDays.HasValue ? p.CountOfPermittedDays.Value : 0);
			return TimeSpan.FromDays(countOfUsedDays);
		}

		public async Task<List<Permission>> GetIntersectingPermissionsByEmployeeIdAsync(Guid? employeeId, DateTime startDate, DateTime endDate)
		{
			return await _context.Permissions
							.Where(p => p.EmployeeId == employeeId)
							.Where(p => (p.StartOfPermissionDate <= endDate && p.EndOfPermissionDate >= startDate))
							.ToListAsync();
		}
	}
}
