

using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Application.DTOs.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public List<CartItemDto> CartItems { get; set; } = new();
    }
}
