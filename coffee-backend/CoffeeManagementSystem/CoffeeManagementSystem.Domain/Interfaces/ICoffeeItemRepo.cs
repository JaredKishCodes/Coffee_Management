

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface ICoffeeItemRepo
    {
        Task<IEnumerable<CoffeeItem>> GetAllCoffeeItemsAsync();
        Task<CoffeeItem?> GetCoffeeItemByIdAsync(int id);
        Task<IEnumerable<CoffeeItem>> GetCoffeeItemsByCategoryIdAsync(int categoryId);
        Task<CoffeeItem> AddCoffeeItemAsync(CoffeeItem coffeeItem); 
        Task<CoffeeItem> UpdateCoffeeItemAsync(CoffeeItem coffeeItem); 
        Task<bool> DeleteCoffeeItemAsync(int id);
    }
}
