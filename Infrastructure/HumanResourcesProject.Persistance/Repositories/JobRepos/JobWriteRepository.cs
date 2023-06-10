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
    public class JobWriteRepository : WriteRepository<Job>, IJobWriteRepository
    {
        public JobWriteRepository(HumanResourcesProjectDbContext context) : base(context)
        {
        }
    }
}
