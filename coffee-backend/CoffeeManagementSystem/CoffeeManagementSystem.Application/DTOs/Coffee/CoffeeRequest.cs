
using System.ComponentModel.DataAnnotations;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Application.DTOs.Coffee
{
    public class CoffeeRequest
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public required Size Size { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public required string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

}
