using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Repositories
{
    public interface IPersonWriteRepository:IWriteRepository<Person>
    {
    }
}
