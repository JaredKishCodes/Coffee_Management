

namespace CoffeeManagementSystem.Application.DTOs.CoffeeInventory
{
    public class CoffeeInventoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
    }
}
