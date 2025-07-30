

using CoffeeManagementSystem.Application.DTOs.Coffee;
using CoffeeManagementSystem.Domain.Entities;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface ICoffeeService
    {
        Task<IEnumerable<CoffeeResponse>> GetAllCoffeeItemsAsync();
        Task<CoffeeResponse?> GetCoffeeItemByIdAsync(int id);
        Task<IEnumerable<CoffeeResponse>> GetCoffeeItemsByCategoryIdAsync(int categoryId);
        Task<CoffeeResponse> AddCoffeeItemAsync(CoffeeRequest coffeeRequest); 
        Task<CoffeeResponse> UpdateCoffeeItemAsync(int id, CoffeeRequest coffeeRequest); 
        Task<bool> DeleteCoffeeItemAsync(int id);
    }
}
