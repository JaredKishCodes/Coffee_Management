 
using CoffeeManagementSystem.Application.DTOs.CoffeeCategory;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Application.DTOs.Coffee
{
    public class CoffeeResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public decimal Price { get; set; }
        public Size Size { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public CoffeeCategoryDTO? Category { get; set; }
    }
}
