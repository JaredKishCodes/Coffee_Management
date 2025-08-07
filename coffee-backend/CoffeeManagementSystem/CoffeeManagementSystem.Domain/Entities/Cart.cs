

using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public required string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<CartItem> CartItems { get; set; } = new();
    }
}
