using Demo.BLL.Models;
using Demo.DAL.Database;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Service
{
    // some function and operation مثلا لي شاشة الفواتير
    // Like CRUD AND Any Service and Operation
    // Service call Models Not Entity , Like DepartmentDTO not Department in Entity
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationContext db;

        public DepartmentService(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<List<Department>> GetAsync(Expression<Func<Department, bool>> filter)
        {
            var result = await db.Departments.Where(filter).ToListAsync();
            return result;
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter)
        {
            var result = await db.Departments.Where(filter).FirstOrDefaultAsync();
            return result;
        }
        
        public async Task CreateAsync(Department department)
        {
            await db.Departments.AddAsync(department);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            // Find old Recoread
            var oldData = await db.Departments.FindAsync(department.Id); // Entity

            oldData.Name = department.Name;
            oldData.Code = department.Code;

            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
             db.Departments.Remove(department);
             await db.SaveChangesAsync();
        }
    }

    // interface : signature

    // SOLID ===> Dependincy Inversion.
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAsync(Expression<Func<Department, bool>> filter);
        public Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter);

        // هتستقبل من Controller Object save it in db.
        public Task CreateAsync(Department department);

        public Task UpdateAsync(Department department);
        public Task DeleteAsync(Department department);

    }
}
