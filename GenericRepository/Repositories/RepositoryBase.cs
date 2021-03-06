﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GenericRepository
{
    public abstract class RepositoryBase<TContext> : IRepositoryInjection where TContext : DbContext
    {
        protected RepositoryBase(TContext context)
        {
            this.Context = context;
        }
        protected RepositoryBase(ILogger<DataAccess> logger, TContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        public IRepositoryInjection SetContext(DbContext context)
        {
            this.Context = (TContext)context;
            return this;
        }
    }
}
