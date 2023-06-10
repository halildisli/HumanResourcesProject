using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories.PersonRepos
{
    public class PersonReadRepository : ReadRepository<Person>, IPersonReadRepository
    {
        public PersonReadRepository(HumanResourcesProjectDbContext context) : base(context)
        {
        }
    }
}
