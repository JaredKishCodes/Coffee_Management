using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly CoffeeDbContext _context;

        public CartRepo(CoffeeDbContext context)
        {
            _context = context;
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
