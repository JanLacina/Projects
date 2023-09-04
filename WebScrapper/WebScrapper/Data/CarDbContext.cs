using Microsoft.EntityFrameworkCore;
using WebScrapper.Entities;

namespace WebScrapper.Data
{
    public partial class CarDbContext : DbContext, IDisposable
    {
        private readonly string _connectionString = "Server=CZ-L-7432572;Database=CarDatabase;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=true";

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.ScrapDate).IsRequired();

                entity.Property(e => e.LinkToDetail).IsRequired();
                entity.Property(e => e.Mileage).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Year).IsRequired();

                entity.HasMany(p => p.Equipment);
                entity.HasMany(p => p.Specification);
            });
        }

        public DbSet<Car> Cars { get; set; }
    }
}
