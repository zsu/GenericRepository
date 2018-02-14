using Example.Entities;
using GenericRepository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    public class AppContext : EntityContextBase<AppContext>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        { }

        public DbSet<Department> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.LowerCaseTablesAndFields();
        }
    }
}
