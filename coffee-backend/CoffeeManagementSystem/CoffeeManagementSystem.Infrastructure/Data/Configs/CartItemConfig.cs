

using CoffeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeManagementSystem.Infrastructure.Data.Configs
{
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Quantity);
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
            builder.Property(x => x.Total).HasPrecision(18, 2);

            builder.HasOne(x => x.Cart)
                   .WithMany(x => x.CartItems)
                   .HasForeignKey(x => x.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CoffeeItem)
                .WithMany()
                .HasForeignKey(x => x.CoffeeItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

