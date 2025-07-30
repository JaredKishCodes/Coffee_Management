

using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementSystem.Application.DTOs.CoffeeCategory
{
    public class CoffeeCategoryReq
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
    }
}
