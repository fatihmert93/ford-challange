using System;
using System.Collections.Generic;
using System.Linq;
using Ford.Core.Helpers;
using Ford.Models;
using Microsoft.EntityFrameworkCore;

namespace Ford.DataAccess
{
    public class FordDbContext : DbContext
    {
        public FordDbContext(DbContextOptions<FordDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:ford-test.database.windows.net,1433;Initial Catalog=ford-test;Persist Security Info=False;User ID=ford-admin;Password=frd31415.FD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> entityTypes = ReflectionUtility.FindSubClassesOf<EntityBase>().ToList();
            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType).ToTable(entityType.Name);
            }
            
            base.OnModelCreating(modelBuilder);
        }
    }
}