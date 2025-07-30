

using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Enums;

namespace CoffeeManagementSystem.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public required string CustomerName { get; set; }
        public ICollection<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
