

using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync();
        Task<CartItemDto?> GetCartItemByIdAsync(int cartItemId);
        Task<CartItemDto?> AddCartItemAsync(AddCartItemDto cartItem);
        Task<CartItemDto?> UpdateCartItemAsync(int cartItemId, int quantity);
        Task<bool> DeleteCartItemAsync(int cartItemId);
    }
}
