

using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Domain.Interfaces
{
    public interface ICoffeeCategoryRepo
    {
        Task<IEnumerable<CoffeeCategory>> GetAllCoffeeCategoriesAsync();
        Task<CoffeeCategory?> GetCoffeeCategoryByIdAsync(int id);
        Task<CoffeeCategory> AddCoffeeCategoryAsync(CoffeeCategory coffeeCategory);
        Task<CoffeeCategory> UpdateCoffeeCategoryAsync(CoffeeCategory coffeeCategory);
        Task<bool> DeleteCoffeeCategoryAsync(int id);
    }
}
