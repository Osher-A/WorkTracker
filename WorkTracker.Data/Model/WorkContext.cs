using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Data.Model
{
    public sealed class WorkContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Work> Works { get; set; }

        public DbSet<WorkDetails> WorksDetails { get; set; }

        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WorkDb"].ConnectionString)
            //   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging(); 
            // optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True;Integrated Security=True; MultipleActiveResultSets = true;Encrypt=True; TrustServerCertificate=True;")
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
