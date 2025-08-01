

using System;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Data
{
    public class CoffeeDbContext : IdentityDbContext<AppUser>
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoffeeCategory> CoffeeCategories { get; set; }
        public DbSet<CoffeeItem> CoffeeItems { get; set; } 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoffeeDbContext).Assembly);


        }
    } 
}

