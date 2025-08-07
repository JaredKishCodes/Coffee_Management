

using CoffeeManagementSystem.Application.DTOs.Order;

namespace CoffeeManagementSystem.Application.DTOs.Cart
{
    public class AddCartDto
    {
      
        public required string CustomerName { get; set; }
        public ICollection<AddCartItemRequest> CartItems { get; set; } = new List<AddCartItemRequest>();

    }
}
