using CellphoneInventory.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CellphoneInventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Device entity
            modelBuilder.Entity<Device>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Device>()
                .Property(d => d.IMEI)
                .IsRequired()
                .HasMaxLength(15);

            // Configure Movement entity
            modelBuilder.Entity<Movement>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Device)
                .WithMany()
                .HasForeignKey(m => m.DeviceId);
        }
    }
}