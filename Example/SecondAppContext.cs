using Example.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    public class SecondAppContext : EntityContextBase<SecondAppContext>
    {
        public SecondAppContext():base(new DbContextOptions<SecondAppContext>())
        { }
        public SecondAppContext(DbContextOptions<SecondAppContext> options) : base(options)
        { }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.LowerCaseTablesAndFields();
        }
    }
}
