using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}