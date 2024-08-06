using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Service
{
    // dependency injection : class and interface
    public class GenericRepossitory<TEntity> : IGenericRepossitory<TEntity> where TEntity : class, new()
    {
        #region Prop
        private readonly ApplicationContext context;

        private DbSet<TEntity> dbSet; 

        #endregion

        #region Ctor
        public GenericRepossitory(ApplicationContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Operations
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, int? page = null, int pageSize = 10, bool noTrack = false, params Expression<Func<TEntity, object>>?[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includePropertie in includeProperties)
                {
                    query = query.Include(includePropertie);
                }
            }
            if (noTrack)
            {
                query = query.AsNoTracking();
            }

            if (page.HasValue && page > 0)  // pagination
            {
                query = query.Skip((page.Value - 1) * pageSize).Take(pageSize);
            }

            // total 100
            // take 10
            // 1 2 3 
             
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAssync(Expression<Func<TEntity, bool>>? filter = null, List<Expression<Func<TEntity, object>>>? includeProperties = null, bool noTrack = false)
        {
            IQueryable<TEntity> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var includePropertie in includeProperties)
                {
                    query = query.Include(includePropertie);
                }
            }
            if(noTrack)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task CreateOrUpdateAsync(TEntity entity)
        {
            var existingEntity = await dbSet.FindAsync(GetKeyValues(entity));

            if(existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(entity);  // edit
                await context.SaveChangesAsync();
            } 
            else
            {
                await dbSet.AddAsync(entity); //add
                await context.SaveChangesAsync();
            }
        }

        // Soft Delete
        public async Task DeleteAsync(TEntity entity)
        {
            await CreateOrUpdateAsync(entity);

            // If you want to delete Data from database, not delete soft
            //dbSet.Remove(entity);
            //await context.SaveChangesAsync();
            // And Delete this line of code : await CreateOrUpdateAsync(entity);

        }

        public object[] GetKeyValues(TEntity entity)
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var Key = entityType.FindPrimaryKey();
            var keyValues = Key.Properties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();
            return keyValues;
        }

        #endregion


    }
    public interface IGenericRepossitory<TEntity> where TEntity : class, new()
    {
        public Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            int? page = null,  // Number of page that will apper data (skip , take in linq  )
            int pageSize = 10,
            bool noTrack = false,
            params Expression<Func<TEntity, object>>?[] includeProperties);

        public Task<TEntity> GetFirstOrDefaultAssync(
             Expression<Func<TEntity, bool>>? filter = null,
             List<Expression<Func<TEntity, object>>>? includeProperties = null,
             bool noTrack = false);

        public Task CreateOrUpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
        public object[] GetKeyValues(TEntity entity);    
    }
}
