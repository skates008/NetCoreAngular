using System;
using NetCoreAngular.Domain; 
using Microsoft.EntityFrameworkCore;

namespace NetCoreAngular.Data
{
    public class NetCoreAngularDbContext  :DbContext
    {


        public NetCoreAngularDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public DbSet<Customer> Customers { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly); 
        }

    }
}
