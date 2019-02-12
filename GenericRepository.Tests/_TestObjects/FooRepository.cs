﻿using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GenericRepository.Tests._TestObjects
{
    public class FooRepository : EntityRepositoryBase<InMemoryContext, Foo>, IFooRepository
    {
        public FooRepository(ILogger<DataAccess> logger, InMemoryContext context) : base(logger, context)
        {
        }
    }
}
