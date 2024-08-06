using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Database.Seed
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        // Seed : البداية 


        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            // مليت بعض البيانات في البداية
            modelBuilder.Entity<Department>().ToTable("Departments")
                .HasData(
                        new Department { Id = 1,  Name = "A" , Code = "IT-BSS-100" },
                        new Department { Id = 2,  Name = "B" , Code = "IT-OSS-200" },
                        new Department { Id = 3,  Name = "C" , Code = "IT-OLS-300" },
                        new Department { Id = 4,  Name = "D" , Code = "IT-USSD-400" }
                );
        }
    }
}
