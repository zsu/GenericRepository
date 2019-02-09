using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IEnumerable<TEntity> GetPage(int startRij, int aantal, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> GetPageAsync(int startRij, int aantal, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity Get(object id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<TEntity> GetAsync(object id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        TEntity Get(object[] key);
        Task<TEntity> GetAsync(object[] key);
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values exists in the context, then it is returned immediately without
        //     making a request to the store. Otherwise, a request is made to the store for
        //     an entity with the given primary key values and this entity, if found, is attached
        //     to the context and returned. If no entity is found in the context or the store,
        //     then null is returned.
        //
        // Parameters:
        //   key:
        //     The values of the primary key for the entity to be found.
        TEntity Find(object[] key);
        // Summary:
        //     Finds an entity with the given primary key values. If an entity with the given
        //     primary key values exists in the context, then it is returned immediately without
        //     making a request to the store. Otherwise, a request is made to the store for
        //     an entity with the given primary key values and this entity, if found, is attached
        //     to the context and returned. If no entity is found in the context or the store,
        //     then null is returned.
        //
        // Parameters:
        //   key:
        //     The values of the primary key for the entity to be found.
        Task<TEntity> FindAsync(object[] key);

        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        IEnumerable<TEntity> QueryPage(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<IEnumerable<TEntity>> QueryPageAsync(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        void Add(TEntity entity);

        /// <summary>
        //     Sets the values of this dictionary by reading values from another dictionary.
        //     The other dictionary must be based on the same type as this dictionary, or a
        //     type derived from the type for this dictionary.
        /// </summary>
        /// <param name="entity">The dictionary to read values from.</param>
        TEntity Update(TEntity entity);
        /// <summary>
        //     Sets the values of this dictionary by reading values out of the given object.
        //     The given object can be of any type. Any property on the object with a name that
        //     matches a property name in the dictionary and can be read will be read. Other
        //     properties will be ignored. This allows, for example, copying of properties from
        //     simple Data Transfer Objects (DTOs).
        /// </summary>
        /// <param name="entity">The dictionary to read values from.</param>
        /// <returns></returns>
        TEntity Update(object entity);
        void Remove(TEntity entity);
        void Remove(params object[] keyValues);

        bool Any(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);

        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        void SetUnchanged(TEntity entitieit);
        void Attach(TEntity entity);
    }
}
