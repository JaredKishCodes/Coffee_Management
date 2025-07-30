

using CoffeeManagementSystem.Application.DTOs.CoffeeCategory;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;


namespace CoffeeManagementSystem.Application.Services
{
    public class CoffeeCategoryService(ICoffeeCategoryRepo _coffeeCategoryRepo): ICoffeeCategoryService
    {
        public async Task<AllCategoriesDto> AddCoffeeCategoryAsync(CoffeeCategoryReq coffeeCategoryReq)
        {
            


            var coffeeCategory = new CoffeeCategory
            {
                Name = coffeeCategoryReq.Name,
                Description = coffeeCategoryReq.Description
            };

            await _coffeeCategoryRepo.AddCoffeeCategoryAsync(coffeeCategory);

            return  new AllCategoriesDto {Id = coffeeCategory.Id, Name = coffeeCategoryReq.Name,Description = coffeeCategoryReq.Description };
        }

        public async Task<bool> DeleteCoffeeCategoryAsync(int id)
        {
            var coffeeCategory = await _coffeeCategoryRepo.GetCoffeeCategoryByIdAsync(id);
            if (coffeeCategory == null)
            {
                return false;
            }

            await _coffeeCategoryRepo.DeleteCoffeeCategoryAsync(id);                        
            return true;
        }

        public async Task<IEnumerable<AllCategoriesDto>> GetAllCoffeeCategoriesAsync()
        {
           var coffeeCategories =  await _coffeeCategoryRepo.GetAllCoffeeCategoriesAsync();

            return coffeeCategories.Select(c => new AllCategoriesDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<AllCategoriesDto?> GetCoffeeCategoryByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentException("Invalid coffee category ID", nameof(id));
           var coffeeCategory =  await _coffeeCategoryRepo.GetCoffeeCategoryByIdAsync(id);

            if (coffeeCategory == null)
            {
                throw new ArgumentException("Coffee Category not found", nameof(id));
            }

            return new AllCategoriesDto
            {
                Id = coffeeCategory.Id,
                Name = coffeeCategory.Name,
                Description = coffeeCategory.Description
            };

        }

        public async Task<AllCategoriesDto> UpdateCoffeeCategoryAsync(int id, CoffeeCategoryReq coffeeCategoryReq)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid Id or Id not found", nameof(id));

            var existingCoffeeCategory = await _coffeeCategoryRepo.GetCoffeeCategoryByIdAsync(id);
            
            if (existingCoffeeCategory == null)
            {
                throw new ArgumentException("Coffee category not found", nameof(id));
            }

            existingCoffeeCategory.Name = coffeeCategoryReq.Name;
            existingCoffeeCategory.Description = coffeeCategoryReq.Description;

            await _coffeeCategoryRepo.UpdateCoffeeCategoryAsync(existingCoffeeCategory);

            return new AllCategoriesDto
            {
                Id = existingCoffeeCategory.Id,
                Name = existingCoffeeCategory.Name,
                Description = existingCoffeeCategory.Description
            };
        }
    }
}
