using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Application.Repositories.AdvanceRepos;
using HumanResourcesProject.Application.Repositories.CompanyRepos;
using HumanResourcesProject.Application.Repositories.DepartmentRepos;
using HumanResourcesProject.Application.Repositories.EmployeeRepos;
using HumanResourcesProject.Application.Repositories.ExpenseRepos;
using HumanResourcesProject.Application.Repositories.JobRepos;
using HumanResourcesProject.Application.Repositories.PermissionRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application
{
	public interface IUnitOfWork : IDisposable
	{
		IAdvanceReadRepository AdvanceReadRepository { get; }
		IAdvanceWriteRepository AdvanceWriteRepository { get; }
		ICompanyReadRepository CompanyReadRepository { get; }
		ICompanyWriteRepository CompanyWriteRepository { get; }
		IDepartmentReadRepository DepartmentReadRepository { get; }
		IDepartmentWriteRepository DepartmentWriteRepository { get; }
		IEmployeeReadRepository EmployeeReadRepository { get; }
		IEmployeeWriteRepository EmployeeWriteRepository { get; }
		IJobReadRepository JobReadRepository { get; }
		IJobWriteRepository JobWriteRepository { get; }
		IPersonReadRepository PersonReadRepository { get; }
		IPersonWriteRepository PersonWriteRepository { get; }
		IPermissionReadRepository PermissionReadRepository { get; }
		IPermissionWriteRepository PermissionWriteRepository { get; }
		IExpenseReadRepository ExpenseReadRepository { get; }
		IExpenseWriteRepository ExpenseWriteRepository { get; }
		int Save();
	}
}
