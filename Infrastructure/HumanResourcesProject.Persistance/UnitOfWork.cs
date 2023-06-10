using HumanResourcesProject.Application;
using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Application.Repositories.AdvanceRepos;
using HumanResourcesProject.Application.Repositories.CompanyRepos;
using HumanResourcesProject.Application.Repositories.DepartmentRepos;
using HumanResourcesProject.Application.Repositories.EmployeeRepos;
using HumanResourcesProject.Application.Repositories.ExpenseRepos;
using HumanResourcesProject.Application.Repositories.JobRepos;
using HumanResourcesProject.Application.Repositories.PermissionRepos;
using HumanResourcesProject.Persistance.Contexts;
using HumanResourcesProject.Persistance.Repositories.AdvanceRepos;
using HumanResourcesProject.Persistance.Repositories.CompanyRepos;
using HumanResourcesProject.Persistance.Repositories.DepartmentRepos;
using HumanResourcesProject.Persistance.Repositories.EmployeeRepos;
using HumanResourcesProject.Persistance.Repositories.ExpenseRepos;
using HumanResourcesProject.Persistance.Repositories.JobRepos;
using HumanResourcesProject.Persistance.Repositories.PermissionRepos;
using HumanResourcesProject.Persistance.Repositories.PersonRepos;

namespace HumanResourcesProject.Persistance
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly HumanResourcesProjectDbContext _context;
        public UnitOfWork(HumanResourcesProjectDbContext context)
        {
			_context = context;
			AdvanceReadRepository = new AdvanceReadRepository(_context);
			AdvanceWriteRepository = new AdvanceWriteRepository(_context);
			CompanyReadRepository = new CompanyReadRepository(_context);
			CompanyWriteRepository = new CompanyWriteRepository(_context);
			DepartmentReadRepository = new DepartmentReadRepository(_context);
			DepartmentWriteRepository = new DepartmentWriteRepository(_context);
			EmployeeReadRepository = new EmployeeReadRepository(_context);
			EmployeeWriteRepository = new EmployeeWriteRepository(_context);
			JobReadRepository = new JobReadRepository(_context);
			JobWriteRepository = new JobWriteRepository(_context);
			PersonReadRepository = new PersonReadRepository(_context);
			PersonWriteRepository = new PersonWriteRepository(_context);
			PermissionReadRepository = new PermissionReadRepository(_context);
			PermissionWriteRepository = new PermissionWriteRepository(_context);
			ExpenseReadRepository = new ExpenseReadRepository(_context);
			ExpenseWriteRepository = new ExpenseWriteRepository(_context);
		}
        public IAdvanceReadRepository AdvanceReadRepository { get; private set; }

		public IAdvanceWriteRepository AdvanceWriteRepository { get; private set; }

		public ICompanyReadRepository CompanyReadRepository { get; private set; }

		public ICompanyWriteRepository CompanyWriteRepository { get; private set; }

		public IDepartmentReadRepository DepartmentReadRepository { get; private set; }

		public IDepartmentWriteRepository DepartmentWriteRepository { get; private set; }

		public IEmployeeReadRepository EmployeeReadRepository { get; private set; }

		public IEmployeeWriteRepository EmployeeWriteRepository { get; private set; }

		public IJobReadRepository JobReadRepository { get; private set; }

		public IJobWriteRepository JobWriteRepository { get; private set; }

		public IPersonReadRepository PersonReadRepository { get; private set; }

		public IPersonWriteRepository PersonWriteRepository { get; private set; }

		public IPermissionReadRepository PermissionReadRepository { get; private set; }

		public IPermissionWriteRepository PermissionWriteRepository { get; private set; }

		public IExpenseReadRepository ExpenseReadRepository { get; private set; }

		public IExpenseWriteRepository ExpenseWriteRepository { get; private set; }

		public int Save()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
