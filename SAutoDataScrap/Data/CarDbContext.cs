using Microsoft.EntityFrameworkCore;
using SAutoDataScrap.Entities;

namespace SAutoDataScrap.Data
{
    public class CarDbContext : DbContext, IDisposable
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
                entity.Property(c => c.ScrapDate).IsRequired();

                entity.Property(c => c.LinkToDetail).IsRequired();
                entity.Property(c => c.Mileage).IsRequired();
                entity.Property(c => c.Price).IsRequired();
                entity.Property(c => c.Year).IsRequired();

                entity.HasMany(e => e.Equipment).WithOne(c => c.Car).HasForeignKey(c => c.CarId);
                entity.HasMany(s => s.Specification).WithOne(c => c.Car).HasForeignKey(c => c.CarId);
            });
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Specification> Specification { get; set; }
    }
}
