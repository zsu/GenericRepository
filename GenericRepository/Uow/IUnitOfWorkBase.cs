using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();
        TRepository GetCustomRepository<TRepository>();
    }
}