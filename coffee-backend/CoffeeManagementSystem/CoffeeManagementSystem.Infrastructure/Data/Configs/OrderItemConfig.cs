

using CoffeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeManagementSystem.Infrastructure.Data.Configs
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Quantity);
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
            builder.Property(x => x.Total).HasPrecision(18, 2);
           

            builder.HasOne(x => x.Order)
                  .WithMany(x => x.OrderItems)
                  .HasForeignKey(x => x.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CoffeeItem)
                .WithMany()
                .HasForeignKey(x => x.CoffeeItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
