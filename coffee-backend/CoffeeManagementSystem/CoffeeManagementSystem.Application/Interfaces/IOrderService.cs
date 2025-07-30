
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto order);
        Task<OrderDto> UpdateOrderAsync(int id,CreateOrderDto order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
