using HumanResourcesProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Contexts
{
    public class HumanResourcesProjectDbContext : IdentityDbContext<Person,PersonRole,string>
    {
        public HumanResourcesProjectDbContext(DbContextOptions<HumanResourcesProjectDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonRole> PersonRoles { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Advance> Advances { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    DummyRolesAdd(builder);
        //}

        //private static void DummyRolesAdd(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PersonRole>().HasData(
        //        new PersonRole() { Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = "10" },
        //        new PersonRole() { Name = "manager", NormalizedName = "MANAGER", ConcurrencyStamp = "10" },
        //        new PersonRole() { Name = "employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = "10" }
        //        );
        //}

    }
}
