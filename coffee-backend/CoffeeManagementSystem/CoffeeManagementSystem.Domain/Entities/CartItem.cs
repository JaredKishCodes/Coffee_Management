

namespace CoffeeManagementSystem.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!; 

        public int CoffeeItemId { get; set; }
        public string? CoffeeItemImg { get; set; }
        public CoffeeItem CoffeeItem { get; set; } = null!; 

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
