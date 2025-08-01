

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface ICartItemRepo
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync();
        Task<CartItem?> GetCartItemByIdAsync(int id);
        Task<CartItem?> AddCartItemAsync(CartItem cartItem);
        Task<CartItem?> UpdateCartItemAsync(int cartItemId, int quantity);
        Task<bool> DeleteCartItemAsync(int cartItemId);
    }
}
