

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId);
        Task<OrderItem?> CreateOrderItemAsync(OrderItem orderItem);
        Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> DeleteOrderItemAsync(int orderItemId);
    }
}
