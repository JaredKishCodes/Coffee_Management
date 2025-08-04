

namespace CoffeeManagementSystem.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int CoffeeItemId { get; set; }
        public string CoffeeItem { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
