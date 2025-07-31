using System.Collections.Generic;

namespace CoffeeManagementSystem.Domain.Entities
{
    public class CoffeeCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<CoffeeItem> CoffeeItems { get; set; } = new List<CoffeeItem>();

    }
}
