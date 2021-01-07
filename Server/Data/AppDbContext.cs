using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Contestant> Contestants { get; set; }
        public DbSet<WeightLog> WeightLogs { get; set; }
        public DbSet<WeighInDeadline> WeighInDeadlines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competition>(c =>
            {
                c.Property(c => c.PlayInAmount).HasColumnType("money");
            });
        }
    }
}
