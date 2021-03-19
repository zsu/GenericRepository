using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class GenericService<TEntityContext> : IGenericService<TEntityContext> where TEntityContext : DbContext//, new()
    {
        private readonly IUowProvider _uowProvider;
        public GenericService()
        {
            _uowProvider = new UowProvider();
        }
        public GenericService(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }
        /// <summary>
        /// To wrap multiple database operations into one transaction, create an explicit UnitOfWork object and pass it along, call SaveChanges() at the end and dispose.
        /// </summary>
        /// <returns></returns>
        public virtual IUnitOfWork CreateUnitOfWork()
        {
            return _uowProvider.CreateUnitOfWork<TEntityContext>();
        }
        public virtual IUnitOfWork CreateUnitOfWork(bool trackChanges)
        {
            return _uowProvider.CreateUnitOfWork<TEntityContext>(trackChanges);
        }
        public virtual string Add<T>(T item) where T : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                repository.Add(item);
                uow.SaveChanges();
            }
            return null;
        }
        public virtual string Add<T>(IUnitOfWork uow, T item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.Add(item);
            return null;
        }
        public virtual string Update<T>(object item) where T : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                repository.Update(item);
                uow.SaveChanges();
            }
            return null;
        }
        public virtual string Update<T>(IUnitOfWork uow, object item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.Update(item);
            return null;
        }
        public T UpdateWithNavigationProperties<T>(T item) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                var result = repository.UpdateWithNavigationProperties(item);
                uow.SaveChanges();
                return result;
            }
        }
        public T UpdateWithNavigationProperties<T>(IUnitOfWork uow, T item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            var result = repository.UpdateWithNavigationProperties(item);
            return result;
        }
        public virtual string Remove<T>(T item) where T : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                repository.Remove(item);
                uow.SaveChanges();
            }
            return null;
        }
        public virtual string Remove<T>(params object[] keys) where T : class, new()
        {
            if (keys == null || keys.Length == 0)
                throw new ArgumentNullException("item");
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                repository.Remove(keys);
                uow.SaveChanges();
            }
            return null;
        }
        public virtual string Remove<T>(IUnitOfWork uow, T item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.Remove(item);
            return null;
        }
        public virtual string Remove<T>(IUnitOfWork uow, params object[] keys) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (keys == null || keys.Length == 0)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.Remove(keys);
            return null;
        }
        public IEnumerable<T> GetAll<T>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.GetAll(orderBy, includes);
            }
        }
        public IEnumerable<T> GetAll<T>(IUnitOfWork uow, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.GetAll(orderBy, includes);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.GetAllAsync(orderBy, includes);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(IUnitOfWork uow, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.GetAllAsync(orderBy, includes);
        }
        public virtual T Get<T>(object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.Get(id, includes);
            }
        }
        public virtual T Get<T>(params object[] keys) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.Get(keys);
            }
        }
        public virtual T Get<T>(IUnitOfWork uow, object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.Get(id, includes);
        }
        public virtual T Get<T>(IUnitOfWork uow, params object[] keys) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.Get(keys);
        }
        public virtual Task<T> GetAsync<T>(object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.GetAsync(id, includes);
            }
        }
        public virtual Task<T> GetAsync<T>(params object[] keys) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.GetAsync(keys);
            }
        }
        public virtual Task<T> GetAsync<T>(IUnitOfWork uow, object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.GetAsync(id, includes);
        }
        public virtual Task<T> GetAsync<T>(IUnitOfWork uow, params object[] keys) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.GetAsync(keys);
        }

        public virtual IEnumerable<T> Query<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.Query(filter, orderBy, includes);
            }
        }
        public virtual IEnumerable<T> Query<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.Query(filter, orderBy, includes);
        }
        public virtual async Task<IEnumerable<T>> QueryAsync<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.QueryAsync(filter, orderBy, includes);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.QueryAsync(filter, orderBy, includes);
        }
        public virtual IEnumerable<T> GetPage<T>(int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.GetPage(startRow, pageLength, orderBy, includes);
            }
        }
        public virtual IEnumerable<T> GetPage<T>(IUnitOfWork uow, int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.GetPage(startRow, pageLength, orderBy, includes);
        }
        public virtual async Task<IEnumerable<T>> GetPageAsync<T>(int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.GetPageAsync(startRow, pageLength, orderBy, includes);
            }
        }
        public virtual async Task<IEnumerable<T>> GetPageAsync<T>(IUnitOfWork uow, int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.GetPageAsync(startRow, pageLength, orderBy, includes);
        }
        public virtual IEnumerable<T> QueryPage<T>(int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.QueryPage(startRow, pageLength, filter, orderBy, includes);
            }
        }
        public virtual IEnumerable<T> QueryPage<T>(IUnitOfWork uow, int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.QueryPage(startRow, pageLength, filter, orderBy, includes);
        }
        public virtual async Task<IEnumerable<T>> QueryPageAsync<T>(int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.QueryPageAsync(startRow, pageLength, filter, orderBy, includes);
            }
        }
        public virtual async Task<IEnumerable<T>> QueryPageAsync<T>(IUnitOfWork uow, int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.QueryPageAsync(startRow, pageLength, filter, orderBy, includes);
        }
        public virtual void Load<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                repository.LoadAsync(filter, orderBy, includes);
            }
        }
        public virtual void Load<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            repository.LoadAsync(filter, orderBy, includes);
        }
        public virtual async Task LoadAsync<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                await repository.LoadAsync(filter, orderBy, includes);
            }
        }
        public virtual async Task LoadAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            await repository.LoadAsync(filter, orderBy, includes);
        }
        public virtual bool Any<T>(Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.Any(filter);
            }
        }
        public virtual bool Any<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.Any(filter);
        }
        public virtual async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.AnyAsync(filter);
            }
        }
        public virtual async Task<bool> AnyAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.AnyAsync(filter);
        }
        public virtual int Count<T>(Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return repository.Count(filter);
            }
        }
        public virtual int Count<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return repository.Count(filter);
        }
        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            using (var uow = CreateUnitOfWork())
            {
                var repository = uow.GetRepository<T>();
                return await repository.CountAsync(filter);
            }
        }
        public virtual async Task<int> CountAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            var repository = uow.GetRepository<T>();
            return await repository.CountAsync(filter);
        }
        public virtual IUnitOfWork SetUnchanged<T>(T item) where T : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var uow = CreateUnitOfWork();
            var repository = uow.GetRepository<T>();
            repository.SetUnchanged(item);
            return uow;
        }
        public virtual void SetUnchanged<T>(IUnitOfWork uow, T item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.SetUnchanged(item);
        }
        public virtual IUnitOfWork Attach<T>(T item) where T : class, new()
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var uow = CreateUnitOfWork();
            var repository = uow.GetRepository<T>();
            repository.Attach(item);
            return uow;
        }
        public virtual void Attach<T>(IUnitOfWork uow, T item) where T : class, new()
        {
            if (uow == null)
                throw new ArgumentNullException("uow");
            if (item == null)
                throw new ArgumentNullException("item");
            var repository = uow.GetRepository<T>();
            repository.Attach(item);
        }
    }
    /*public class GenericService<TEntityContext> : GenericService, IGenericService<TEntityContext> where TEntityContext : DbContext, new()
    {
        private readonly IUowProvider _uowProvider;
        public GenericService(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }
        public override IUnitOfWork CreateUnitOfWork()
        {
            return _uowProvider.CreateUnitOfWork<TEntityContext>();
        }
        public override IUnitOfWork CreateUnitOfWork(bool trackChanges)
        {
            return _uowProvider.CreateUnitOfWork<TEntityContext>(trackChanges);
        }
    }*/
}
