
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CartItemRepo(CoffeeDbContext _context) : ICartItemRepo
    {
        public async Task<CartItem?> AddCartItemAsync(CartItem cartItem)
        {
          await  _context.CartItems.AddAsync(cartItem);
          await _context.SaveChangesAsync();

            return await _context.CartItems
        .Include(c => c.Cart)
        .Include(c => c.CoffeeItem)
        .FirstOrDefaultAsync(c => c.Id == cartItem.Id);
        }

        public async Task<bool> DeleteCartItemAsync(int cartItemId)
        {
            var cart =await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);
            if (cart != null)
            { 
                _context.CartItems.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartItem?> GetCartItemByIdAsync(int id)
        {
            var cart = await _context.CartItems.Include(x => x.CoffeeItem).FirstOrDefaultAsync(x => x.Id == id);
            return cart;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.Include(x => x.CoffeeItem).ToListAsync();
        }

        public async Task<CartItem?> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems
                .Include(x => x.CoffeeItem)
                .FirstOrDefaultAsync(x => x.Id == cartItemId);

            if (cartItem == null)
                return null;

            cartItem.Quantity = quantity;
            cartItem.Total = cartItem.CoffeeItem.Price * quantity;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return cartItem;
        }

    }
}
