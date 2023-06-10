using HumanResourcesProject.Application.Repositories.AdvanceRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.AdvanceRepos
{
    public class AdvanceWriteRepository : WriteRepository<Advance>, IAdvanceWriteRepository
    {
        public AdvanceWriteRepository(HumanResourcesProjectDbContext context) : base(context)
        {

        }
    }
}
