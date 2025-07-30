

namespace CoffeeManagementSystem.Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int CoffeeItemId { get; set; }
        public string CoffeeName { get; set; } = string.Empty; 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }

}
