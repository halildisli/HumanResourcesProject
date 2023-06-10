using HumanResourcesProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HumanResourcesProjectDbContext>
    {
        public HumanResourcesProjectDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<HumanResourcesProjectDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(MyConfiguration.ConnectionString);
            return new HumanResourcesProjectDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
