
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CartItemRepo(CoffeeDbContext _context) : ICartItemRepo
    {
        public Task<CartItem?> AddCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCartItemAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem?> GetCartItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CartItem?> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
