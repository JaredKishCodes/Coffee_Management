using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public required string CustomerName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
