

using CoffeeManagementSystem.Application.Interfaces.Auth;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Auth.Service;
using CoffeeManagementSystem.Infrastructure.Data;
using CoffeeManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<CoffeeDbContext>(options =>
                {
                options.UseSqlServer(config.GetConnectionString("CoffeeDbConnection"));
            });

            services.AddScoped<ICoffeeItemRepo, CoffeeRepo>();
            services.AddScoped<ICoffeeCategoryRepo, CoffeeCategoryRepo>();
            services.AddScoped<ICoffeeInventoryRepo, CoffeeInventoryRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartItemRepo, CartItemRepo>();
            services.AddScoped<ICartRepo, CartRepo>();
        



            return services;
        }
    }
}
