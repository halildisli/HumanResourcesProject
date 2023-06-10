using HumanResourcesProject.Application.Repositories.DepartmentRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.DepartmentRepos
{
    public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
    {
        public DepartmentWriteRepository(HumanResourcesProjectDbContext context) : base(context)
        {
        }
    }
}
