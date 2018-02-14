using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Context
{
    public class EntityContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public EntityContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
