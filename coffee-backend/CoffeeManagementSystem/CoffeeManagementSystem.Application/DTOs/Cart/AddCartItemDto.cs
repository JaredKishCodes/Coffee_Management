

namespace CoffeeManagementSystem.Application.DTOs.Cart
{
    public class AddCartItemDto
    {
        public int CoffeeItemId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
