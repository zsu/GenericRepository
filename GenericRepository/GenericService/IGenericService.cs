using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IGenericService<TEntityContext> where TEntityContext : DbContext//, new()
    {
        IUnitOfWork CreateUnitOfWork();
        IUnitOfWork CreateUnitOfWork(bool trackChanges);
        string Add<T>(T item) where T : class, new();
        string Add<T>(IUnitOfWork uow, T item) where T : class, new();
        bool Any<T>(Expression<Func<T, bool>> filter = null) where T : class, new();
        bool Any<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new();
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> filter = null) where T : class, new();
        Task<bool> AnyAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new();
        IUnitOfWork Attach<T>(T item) where T : class, new();
        void Attach<T>(IUnitOfWork uow, T item) where T : class, new();
        int Count<T>(Expression<Func<T, bool>> filter = null) where T : class, new();
        int Count<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new();
        Task<int> CountAsync<T>(Expression<Func<T, bool>> filter = null) where T : class, new();
        Task<int> CountAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter = null) where T : class, new();
        T Get<T>(object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        T Get<T>(params object[] keys) where T : class, new();
        T Get<T>(IUnitOfWork uow, object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        T Get<T>(IUnitOfWork uow, params object[] keys) where T : class, new();
        Task<T> GetAsync<T>(object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<T> GetAsync<T>(params object[] keys) where T : class, new();
        Task<T> GetAsync<T>(IUnitOfWork uow, object id, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<T> GetAsync<T>(IUnitOfWork uow, params object[] keys) where T : class, new();
        IEnumerable<T> GetAll<T>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> GetAll<T>(IUnitOfWork uow, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> GetAllAsync<T>(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> GetAllAsync<T>(IUnitOfWork uow, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> GetPage<T>(int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> GetPage<T>(IUnitOfWork uow, int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> GetPageAsync<T>(int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> GetPageAsync<T>(IUnitOfWork uow, int startRow, int pageLength, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        void Load<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        void Load<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task LoadAsync<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task LoadAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> Query<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T>(IUnitOfWork uow, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> QueryPage<T>(int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        IEnumerable<T> QueryPage<T>(IUnitOfWork uow, int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> QueryPageAsync<T>(int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        Task<IEnumerable<T>> QueryPageAsync<T>(IUnitOfWork uow, int startRow, int pageLength, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null) where T : class, new();
        string Remove<T>(params object[] keys) where T : class, new();
        string Remove<T>(T item) where T : class, new();
        string Remove<T>(IUnitOfWork uow, params object[] keys) where T : class, new();
        string Remove<T>(IUnitOfWork uow, T item) where T : class, new();
        IUnitOfWork SetUnchanged<T>(T item) where T : class, new();
        void SetUnchanged<T>(IUnitOfWork uow, T item) where T : class, new();
        string Update<T>(object item) where T : class, new();
        string Update<T>(IUnitOfWork uow, object item) where T : class, new();
        T UpdateWithNavigationProperties<T>(T item) where T : class, new();
        T UpdateWithNavigationProperties<T>(IUnitOfWork uow, T item) where T : class, new();
    }
    //public interface IGenericService<TEntityContext> : IGenericService where TEntityContext : DbContext, new() { }
}