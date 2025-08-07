

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface ICartRepo
    {
        Task<IEnumerable<Cart>> GetCartsAsync();
        Task<Cart?> GetCartByIdAsync(int cartId);
        Task<Cart?> GetOrCreateCartAsync();
        Task<Cart?> AddCartAsync(Cart cart);
        Task<Cart?> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int id);
    }
}
