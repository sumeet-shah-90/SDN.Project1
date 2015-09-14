using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project1.Data.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Project1.API.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<UserAccessToken> AccessTokens { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Share> Shares { get; set; }

        public DbSet<Changeset> Changesets { get; set; }
    }
}
