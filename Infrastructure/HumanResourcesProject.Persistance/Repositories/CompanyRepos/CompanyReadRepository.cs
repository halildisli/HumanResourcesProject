using HumanResourcesProject.Application.Repositories.CompanyRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.CompanyRepos
{
    public class CompanyReadRepository : ReadRepository<Company>, ICompanyReadRepository
    {
        public CompanyReadRepository(HumanResourcesProjectDbContext context) : base(context)
        {
        }
    }
}
