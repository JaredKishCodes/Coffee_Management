using System;
using CoffeeManagementSystem.Application;
using CoffeeManagementSystem.Infrastructure;
using CoffeeManagementSystem.Infrastructure.Auth;
using CoffeeManagementSystem.Infrastructure.Data;
using MathNet.Numerics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeManagementSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddInfrastructureServices(configuration)
                    .AddApplicationServices() ;

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()      // Allow all origins
                          .AllowAnyMethod()      // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                          .AllowAnyHeader();     // Allow any header
                });
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<CoffeeDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = configuration["JWT:Issuer"],
                 ValidAudience = configuration["JWT:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(
                     System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]))
             });

            return services;
        }
    }
}
