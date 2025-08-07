using System.Security.Claims;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly CoffeeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepo(CoffeeDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart?> AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            return await _context.Carts
             .Include(c => c.CartItems)
             .ThenInclude(ci => ci.CoffeeItem)
             .FirstOrDefaultAsync(c => c.Id == cart.Id);

        }

        public async Task<bool> DeleteCartAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart?> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.CoffeeItem)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.CoffeeItem)
                .ToListAsync();
        }

        public async Task<Cart?> GetOrCreateCartAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? customerName = user.Identity?.Name; // Or use ClaimTypes.Name

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User ID not found in claims");

            var existingCart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);

            if (existingCart != null) return existingCart;

            var newCart = new Cart
            {
                CustomerName = customerName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                CartItems = new List<CartItem>()
            };

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();


            return newCart;

        }

        public async Task<Cart?> UpdateCartAsync(Cart cart)
        {
            var existingCart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existingCart == null) return null;

            existingCart.CustomerName = cart.CustomerName;

            await _context.SaveChangesAsync();
            return existingCart;
        }
    }
}
