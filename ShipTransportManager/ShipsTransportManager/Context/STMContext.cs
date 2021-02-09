using Microsoft.EntityFrameworkCore;
using ShipsTransportManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.Context
{
    public class STMContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Planet> Planets { get; set; }
        
        public STMContext(DbContextOptions option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>()
                .HasOne<Planet>(p => p.Planet)
                .WithMany(s => s.ListOfShips);
        }
    }
}
