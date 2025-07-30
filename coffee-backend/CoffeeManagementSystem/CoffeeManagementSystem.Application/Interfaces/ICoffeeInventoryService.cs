

using CoffeeManagementSystem.Application.DTOs.CoffeeInventory;
using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface ICoffeeInventoryService
    {
        Task<IEnumerable<CoffeeInventoryDto>> GetInventoryByCategoryAsync(int categoryId);
        Task<CoffeeInventoryDto?> UpdateStockAsync(int id, UpdateCoffeeStockRequest updateCoffeeStockRequest);

        Task<CoffeeInventoryDto?> GetInventoryByIdAsync(int itemId);

        Task<IEnumerable<CoffeeInventoryDto>> GetAllInventoryAsync();

    }
}
