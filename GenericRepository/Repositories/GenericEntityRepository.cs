using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace GenericRepository
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : class, new()
    {
        public GenericEntityRepository() : base(null)
        { }
        public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}