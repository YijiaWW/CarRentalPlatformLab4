using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Vehicle>(entity => {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.VehicleCode)
                      .IsRequired()
                      .HasMaxLength(50); // Typical constraint

                entity.Property(e => e.LocationId)
                      .IsRequired();

                entity.Property(e => e.VehicleType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Status)
                      .HasConversion<string>() // Store Enum as String in DB
                      .IsRequired();
            });
        }
    }
}
