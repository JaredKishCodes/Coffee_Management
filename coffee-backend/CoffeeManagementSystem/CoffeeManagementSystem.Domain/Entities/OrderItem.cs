namespace CoffeeManagementSystem.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!; // Navigation to parent Order

        public int CoffeeItemId { get; set; }
        public CoffeeItem CoffeeItem { get; set; } = null!; // Navigation to CoffeeItem

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
