using Microsoft.EntityFrameworkCore;
using TesteT2S.WebApi.Features.Containers.Models;

namespace TesteT2S.WebApi.Features.Containers.Data
{
    public class ContainerContext : DbContext
    {
        public ContainerContext(DbContextOptions<ContainerContext> options) : base(options) { }

        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Container>(entity =>
            {
                entity.Property(container => container.Id)
                    .UseIdentityColumn();
                entity.Property(container => container.Number)
                    .HasMaxLength(Constants.ContainerNumberMaxLenght)
                    .IsRequired();
                entity.HasAlternateKey(container => container.Number);
                entity.Property(container => container.Customer)
                    .HasMaxLength(Constants.CustomerNameMaxLenght)
                    .IsRequired();
                entity.Property(container => container.Type)
                    .IsRequired();
                entity.Property(container => container.Status)
                    .HasConversion<byte>()
                    .IsRequired();
                entity.Property(container => container.Category)
                    .HasConversion<byte>()
                    .IsRequired();

            });
        }
    }
}
