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
            var ConnectionString =
                "Server=tcp:ford-test.database.windows.net,1433;Initial Catalog=ford-test;Persist Security Info=False;User ID=ford-admin;Password=frd31415.FD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(ConnectionString);
            return;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=superdoctor.cnc1ajzvqewo.eu-central-1.rds.amazonaws.com;database=superdoctor;user id=spadmin;password=spr31415SD;");
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