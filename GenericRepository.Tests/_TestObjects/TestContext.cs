using GenericRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Tests
{
    public class TestContext : EntityContextBase<TestContext>
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }
    }
}
