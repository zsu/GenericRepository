using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GenericRepository.Entities;
using System;

namespace GenericRepository.Repositories
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : class, new()
    {
		public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}