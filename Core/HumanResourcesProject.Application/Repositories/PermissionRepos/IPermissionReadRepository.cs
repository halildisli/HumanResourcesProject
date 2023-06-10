using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Repositories.PermissionRepos
{
	public interface IPermissionReadRepository : IReadRepository<Permission>
	{
		Task<TimeSpan> GetCountOfUsedDaysByEmployeeIdAsync(Guid? ıd);
		Task<List<Permission>> GetIntersectingPermissionsByEmployeeIdAsync(Guid? employeeId, DateTime startDate, DateTime endDate);
	}
}
