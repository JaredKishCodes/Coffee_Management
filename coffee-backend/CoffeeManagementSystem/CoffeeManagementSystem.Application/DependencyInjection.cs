using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
             services.AddScoped<ICoffeeService, CoffeeService>();
             services.AddScoped<ICoffeeCategoryService, CoffeeCategoryService>();
             services.AddScoped<ICoffeeInventoryService, CoffeeInventoryService>();
             services.AddScoped<IOrderService, OrderService>();
             services.AddScoped<ICartItemService, CartItemService>();
             services.AddScoped<ICartService, CartService>();

            return services;
        }
    }
}
