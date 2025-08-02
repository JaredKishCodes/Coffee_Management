



namespace CoffeeManagementSystem.Application.DTOs.Cart
{
    public class AddCartDto
    {
        public required string CustomerName { get; set; }
        public ICollection<AddCartItemDto> CartItems { get; set; } = new List<AddCartItemDto>();
    }
}
