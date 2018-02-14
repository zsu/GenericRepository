using System;
using System.Threading;
using System.Threading.Tasks;
using GenericRepository.Entities;
using GenericRepository.Repositories;

namespace GenericRepository.Uow
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity,TKey> GetRepository<TEntity,TKey>();
        TRepository GetCustomRepository<TRepository>();
    }
}