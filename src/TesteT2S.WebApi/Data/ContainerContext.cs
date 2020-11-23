using Microsoft.EntityFrameworkCore;
using TesteT2S.WebApi.Features.Containers.Models;
using TesteT2S.WebApi.Features.ShipHandling.Models;

namespace TesteT2S.WebApi.Data
{
    public class ContainerContext : DbContext
    {
        public ContainerContext(DbContextOptions<ContainerContext> options) : base(options) { }

        public DbSet<Container> Containers { get; set; }
        public DbSet<Handling> Handlings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Container>(entity =>
            {
                entity.Property(container => container.Id)
                    .UseIdentityColumn();
                entity.Property(container => container.Number)
                    .HasMaxLength(Features.Containers.Constants.ContainerNumberMaxLenght)
                    .IsRequired();
                entity.HasAlternateKey(container => container.Number);
                entity.Property(container => container.Customer)
                    .HasMaxLength(Features.Containers.Constants.CustomerNameMaxLenght)
                    .IsRequired();
                entity.Property(container => container.Type)
                    .IsRequired();
                entity.Property(container => container.Status)
                    .HasConversion<byte>()
                    .IsRequired();
                entity.Property(container => container.Category)
                    .HasConversion<byte>()
                    .IsRequired();
                entity.HasMany(container => container.Handlings)
                    .WithOne(handling => handling.Container);

            });
            modelBuilder.Entity<Handling>(entity =>
            {
                entity.Property(handling => handling.Id)
                    .UseIdentityColumn();
                entity.Property(handling => handling.Ship)
                    .HasMaxLength(Features.ShipHandling.Constants.ShipMaxLength)
                    .IsRequired();
                entity.Property(handling => handling.HandlingType)
                    .HasConversion<byte>()
                    .IsRequired();
                entity.Property(handling => handling.Start)
                    .IsRequired();
                entity.Property(handling => handling.End)
                    .IsRequired();
            });
        }
    }
}
