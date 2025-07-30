using CoffeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeManagementSystem.Infrastructure.Data.Configs
{
    public class CoffeeItemConfig : IEntityTypeConfiguration<CoffeeItem>
    {
        public void Configure(EntityTypeBuilder<CoffeeItem> builder)
        {
            builder.ToTable("CoffeeItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.IsAvailable);
            builder.Property(x => x.ImageUrl);

            builder.Property(c => c.Price)
                   .HasPrecision(18, 2);

            builder.HasOne(c => c.Category)
                   .WithMany(cat => cat.CoffeeItems)
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
