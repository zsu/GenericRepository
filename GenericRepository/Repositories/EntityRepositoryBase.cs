using GenericRepository.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public abstract class EntityRepositoryBase<TContext, TEntity> : RepositoryBase<TContext>, IRepository<TEntity> where TContext : DbContext where TEntity : class, new()
    {
        private readonly OrderBy<TEntity> DefaultOrderBy = null;// new OrderBy<TEntity>(qry => qry.OrderBy(e => e.Id));

        protected EntityRepositoryBase(ILogger<DataAccess> logger, TContext context) : base(logger, context)
        { }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (orderBy == null) orderBy = DefaultOrderBy?.Expression;

            var result = QueryDb(null, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (orderBy == null) orderBy = DefaultOrderBy?.Expression;

            var result = QueryDb(null, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual TEntity Get(object id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }
            //if(typeof(TEntity).IsSubclassOf(typeof(Entity<>)))
            //    return query.SingleOrDefault(x => x.Id.Equals(id));
            var properties = GetKeyProperties();
            if (properties.Count() != 1 || !(properties.First().PropertyType == id.GetType()))
                throw new Exception(string.Format("Invalid key type {0}.", id == null ? null : id.GetType().Name));
            return query.SingleOrDefault(x => (x.GetType().GetProperty(properties.First().Name).GetValue(x, null)).Equals(id));
        }
        public virtual TEntity Get(params object[] key)
        {
            return Find(key);
        }

        public virtual async Task<TEntity> GetAsync(object id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            //if (typeof(TEntity).IsSubclassOf(typeof(Entity<>)))
            //    return query.SingleOrDefaultAsync(x => x.Id.Equals(id));
            var properties = GetKeyProperties();
            if (properties.Count() != 1 || !(properties.First().PropertyType == id.GetType()))
                throw new Exception(string.Format("Invalid key type {0}.", id == null ? null : id.GetType().Name));
            return await query.SingleOrDefaultAsync(x => (x.GetType().GetProperty(properties.First().Name).GetValue(x, null)).Equals(id));

        }
        public virtual async Task<TEntity> GetAsync(params object[] key)
        {
            return await FindAsync(key);
        }
        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> QueryPage(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (orderBy == null) orderBy = DefaultOrderBy?.Expression;

            var result = QueryDb(filter, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryPageAsync(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            if (orderBy == null) orderBy = DefaultOrderBy?.Expression;

            var result = QueryDb(filter, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            Context.Set<TEntity>().Add(entity);
        }
        public virtual TEntity Update(object entity)
        {
            List<object> keyValues = new List<object>();
            var properties = GetKeyProperties();
            if (properties.Count() == 0)
                throw new Exception(string.Format("No Key for entity {0}.", typeof(TEntity).Name));
            foreach (var key in properties)
            {
                keyValues.Add(entity.GetType().GetProperty(key.Name).GetValue(entity));
            }

            var existing = Context.Set<TEntity>().Find(keyValues.ToArray());
            if (existing == null) throw new Exception(string.Format("Cannot find entity type {0} with key {1}", typeof(TEntity).Name, string.Join(",", keyValues.ToArray())));
            Context.Entry(existing).CurrentValues.SetValues(entity);
            return existing;
        }
        public virtual TEntity UpdateWithNavigationProperties(TEntity entity)
        {
            Context.Update(entity);
            return entity;
        }
        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove(params object[] keyValues)
        {
            var entity = new TEntity();
            var properties = GetKeyProperties();
            //if (properties.Count() != 1 || !(properties.First().PropertyType == id.GetType()))
            //    throw new Exception(string.Format("Invalid key type {0}.", id == null ? null : id.GetType().Name));
            if (properties.Count() != keyValues.Count())
                throw new Exception("Wrong number of key values.");
            for (int i = 0; i < properties.Count(); i++)
            {
                var key = properties.ElementAt(i);
                entity.GetType().GetProperty(key.Name).SetValue(entity, keyValues[i]);
            }

            this.Remove(entity);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AnyAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }
        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }
        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void SetUnchanged(TEntity entity)
        {
            base.Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }
        private TEntity Find(params object[] key)
        {
            var properties = GetKeyProperties();
            if (properties.Count() == 0)
                throw new Exception(string.Format("No Key for entity {0}.", typeof(TEntity).Name));
            if (properties.Count() != key.Count())
                throw new Exception("Key propertyies number mismatches.");
            return Context.Set<TEntity>().Find(key);
        }
        private async Task<TEntity> FindAsync(params object[] key)
        {
            var properties = GetKeyProperties();
            if (properties.Count() == 0)
                throw new Exception(string.Format("No Key for entity {0}.", typeof(TEntity).Name));
            if (properties.Count() != key.Count())
                throw new Exception("Key propertyies number mismatches.");
            return await Context.Set<TEntity>().FindAsync(key);
        }
        protected virtual IEnumerable<PropertyInfo> GetKeyProperties()
        {
            var properties = typeof(TEntity).GetProperties().Where(prop => prop.IsDefined(typeof(KeyAttribute), true));
            return properties;
        }
    }
}