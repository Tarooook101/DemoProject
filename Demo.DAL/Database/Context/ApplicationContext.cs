using Demo.DAL.Database.Seed;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Database
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> ops) : base(ops) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DbInitializer(modelBuilder).Seed();
        }
    }
    public partial class ApplicationContext
    {
        // DbSet ===> Can make Operation On Table. ( CRUD )
        public DbSet<Department> Departments { get; set; }  // Table
        public DbSet<Employee> Employees { get; set; }
    }
}
