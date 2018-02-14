using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GenericRepository.Entities;
using System;

namespace GenericRepository.Repositories
{
    public class GenericEntityRepository<TEntity,TKey> : EntityRepositoryBase<DbContext, TEntity, TKey> where TEntity : Entity<TKey>, new() where TKey: IComparable
    {
		public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}