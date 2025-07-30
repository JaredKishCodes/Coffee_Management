
using CoffeeManagementSystem.Application.DTOs.CoffeeCategory;

namespace CoffeeManagementSystem.Application.Interfaces
{
    public interface ICoffeeCategoryService
    {
        Task<IEnumerable<AllCategoriesDto>> GetAllCoffeeCategoriesAsync();
        Task<AllCategoriesDto?> GetCoffeeCategoryByIdAsync(int id);
        Task<AllCategoriesDto> AddCoffeeCategoryAsync(CoffeeCategoryReq coffeeCategoryReq);
        Task<AllCategoriesDto> UpdateCoffeeCategoryAsync(int id, CoffeeCategoryReq coffeeCategoryReq);
        Task<bool> DeleteCoffeeCategoryAsync(int id);
    }       
}
