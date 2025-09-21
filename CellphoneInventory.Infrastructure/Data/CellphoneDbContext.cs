using CellphoneInventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CellphoneInventory.Infrastructure.Data
{
    public class CellphoneDbContext : DbContext
    {
        public CellphoneDbContext(DbContextOptions<CellphoneDbContext> options) : base(options)
        {
        }

        public DbSet<Cellphone> Cellphones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Cellphone
            modelBuilder.Entity<Cellphone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IMEI).IsRequired().HasMaxLength(15);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Color).HasMaxLength(30);
                entity.Property(e => e.Storage).HasMaxLength(20);
                entity.Property(e => e.Processor).HasMaxLength(50);
                entity.Property(e => e.RAM).HasMaxLength(20);
            });

            // Datos de ejemplo (opcional)
            modelBuilder.Entity<Cellphone>().HasData(
                new Cellphone
                {
                    Id = 1,
                    Brand = "Samsung",
                    Model = "Galaxy S23",
                    IMEI = "123456789012345",
                    Price = 999.99m,
                    Color = "Negro",
                    Storage = "256GB",
                    Processor = "Snapdragon 8 Gen 2",
                    RAM = "8GB",
                    ReleaseDate = new DateTime(2023, 2, 1),
                    IsAvailable = true
                },
                new Cellphone
                {
                    Id = 2,
                    Brand = "Apple",
                    Model = "iPhone 15 Pro",
                    IMEI = "987654321098765",
                    Price = 1199.99m,
                    Color = "Azul",
                    Storage = "512GB",
                    Processor = "A17 Pro",
                    RAM = "8GB",
                    ReleaseDate = new DateTime(2023, 9, 22),
                    IsAvailable = true
                }
            );
        }
    }
}