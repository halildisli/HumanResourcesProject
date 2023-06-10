using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Application.Repositories.JobRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.JobRepos
{
    public class JobReadRepository : ReadRepository<Job>, IJobReadRepository
    {
        public JobReadRepository(HumanResourcesProjectDbContext context) : base(context)
        {
        }
    }
}
