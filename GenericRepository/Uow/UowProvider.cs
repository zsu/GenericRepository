﻿using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public class UowProvider : IUowProvider
    {
        public UowProvider()
        {
        }
        public UowProvider(ILogger<DataAccess> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false)
        {
            var _context = (DbContext)_serviceProvider.GetService(typeof(IEntityContext));

            if ( !trackChanges )
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }

        public IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : DbContext
        {
            var _context = _serviceProvider==null? (TEntityContext)Activator.CreateInstance(typeof(TEntityContext), new DbContextOptions<TEntityContext>()) : (TEntityContext)_serviceProvider.GetService(typeof(TEntityContext));
            if (!trackChanges)
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = _serviceProvider == null ? new UnitOfWork(_context):new UnitOfWork(_context, _serviceProvider);
            return uow;
        }
    }
}
