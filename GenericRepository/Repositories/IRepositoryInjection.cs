using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}