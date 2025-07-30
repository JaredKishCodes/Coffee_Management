

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId);
        Task<OrderItem?> CreateOrderItemAsync(OrderItem orderItem);
        Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> DeleteOrderItemAsync(int orderItemId);
    }
}
