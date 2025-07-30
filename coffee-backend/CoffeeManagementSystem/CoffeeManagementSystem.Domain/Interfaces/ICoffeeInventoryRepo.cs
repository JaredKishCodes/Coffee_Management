

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface ICoffeeInventoryRepo
    {
        Task<IEnumerable<CoffeeItem>> GetInventoryByCategoryAsync(int categoryId);
        Task<CoffeeItem?> UpdateStockAsync(int id, int newStock);

        Task<CoffeeItem?> GetInventoryByIdAsync(int itemId);

        Task<IEnumerable<CoffeeItem>> GetAllInventoryAsync();

    }
}
