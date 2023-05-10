﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTracker.Model
{
    public sealed class WorkContext : DbContext
    {
        public DbSet<Work> Works { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WorkDb"].ConnectionString)
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).EnableSensitiveDataLogging(); ;
            // optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database = IWagesManager; Trusted_Connection = True;Integrated Security=True; MultipleActiveResultSets = true;Encrypt=True; TrustServerCertificate=True;")
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
