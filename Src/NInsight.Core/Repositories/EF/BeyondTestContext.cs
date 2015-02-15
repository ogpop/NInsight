using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using NInsight.Core.Domain;

namespace NInsight.Core.Repositories.EF
{
    public class BeyondTestContext : DbContext
    {
        public BeyondTestContext()
            : base("TestBeyondContext")
        {
            this.Database.Log = Console.WriteLine;
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Run> Runs { get; set; }

        public DbSet<Point> Points { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
    }
}