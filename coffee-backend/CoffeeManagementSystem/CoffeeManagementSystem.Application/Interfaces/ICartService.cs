

using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetCartsAsync();
        Task<CartDto?> GetCartByIdAsync(int cartId);
        Task<CartDto?> AddCartAsync(AddCartDto addCartDto);
        Task<CartDto?> UpdateCartAsync( int id ,AddCartDto addCartDto);
        Task<bool> DeleteCartAsync(int id);
    }
}
