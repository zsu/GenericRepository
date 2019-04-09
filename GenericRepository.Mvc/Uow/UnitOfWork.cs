using System;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Mvc
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context) : base(context)
        { }
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}
