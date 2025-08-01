

using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public required string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public List<CartItem> CartItems { get; set; } = new();
    }
}
