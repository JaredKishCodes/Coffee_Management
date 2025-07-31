using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeManagementSystem.Infrastructure.Data.Configs
{
    public class CoffeeCategoryConfig : IEntityTypeConfiguration<CoffeeCategory>
    {
        public void Configure(EntityTypeBuilder<CoffeeCategory> builder)
        {
            builder.ToTable("CoffeeCategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(800);

            builder.HasMany(c => c.CoffeeItems)
                  .WithOne(i => i.Category)
                  .HasForeignKey(i => i.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
