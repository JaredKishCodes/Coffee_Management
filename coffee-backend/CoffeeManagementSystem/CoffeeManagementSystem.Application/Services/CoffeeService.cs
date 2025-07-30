
using CoffeeManagementSystem.Application.DTOs.Coffee;
using CoffeeManagementSystem.Application.DTOs.CoffeeCategory;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;

namespace CoffeeManagementSystem.Application.Services
{
   
    public class CoffeeService(ICoffeeItemRepo _coffeeRepo) : ICoffeeService
    {
        public async Task<CoffeeResponse> AddCoffeeItemAsync(CoffeeRequest coffeeRequest)
        {
            var newCoffee = new CoffeeItem
            {
                Name = coffeeRequest.Name,
                Description = coffeeRequest.Description,
                Price = coffeeRequest.Price,
                Size = coffeeRequest.Size,
                Stock = coffeeRequest.Stock,
                IsAvailable = coffeeRequest.IsAvailable,
                ImageUrl = coffeeRequest.ImageUrl,
                CategoryId = coffeeRequest.CategoryId
            };

            await _coffeeRepo.AddCoffeeItemAsync(newCoffee);

            return new CoffeeResponse
            {
                Id = newCoffee.Id, 
                Name = newCoffee.Name,
                Description = newCoffee.Description,
                Price = newCoffee.Price,
                Size = newCoffee.Size,
                Stock = newCoffee.Stock,
                IsAvailable = newCoffee.IsAvailable,
                ImageUrl = newCoffee.ImageUrl,
                CategoryId = newCoffee.CategoryId
            };
        }

        public async Task<bool> DeleteCoffeeItemAsync(int id)
        {
            return await _coffeeRepo.DeleteCoffeeItemAsync(id);
        }


        public async Task<IEnumerable<CoffeeResponse>> GetAllCoffeeItemsAsync()
        {
           var coffee = await _coffeeRepo.GetAllCoffeeItemsAsync();
            return coffee.Select(c => new CoffeeResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                Size = c.Size,
                Stock = c.Stock,
                IsAvailable = c.IsAvailable,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId,
                Category = new CoffeeCategoryDTO
                {
                    Id = c.Category?.Id ?? 0,
                    Name = c.Category?.Name ?? string.Empty,
                }
            });
        }

        public async Task<CoffeeResponse?> GetCoffeeItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("No coffee item ID.", nameof(id));
            }
            var coffee = await _coffeeRepo.GetCoffeeItemByIdAsync(id);

            if (coffee is null)
            {
                throw new Exception("No coffee item.");
            }

            return new CoffeeResponse
            {
                Id = coffee.Id,
                Name = coffee.Name,
                Description = coffee.Description,
                Price = coffee.Price,
                Size = coffee.Size,
                Stock =  coffee.Stock,
                IsAvailable = coffee.IsAvailable,
                ImageUrl = coffee.ImageUrl,
                CategoryId = coffee.CategoryId,
                Category = new CoffeeCategoryDTO
                {
                    Id = coffee.Category?.Id ?? 0,
                    Name = coffee.Category?.Name ?? string.Empty,
                }
            };


        }

        public async Task<IEnumerable<CoffeeResponse>> GetCoffeeItemsByCategoryIdAsync(int categoryId)
        {
            if(categoryId <= 0)
            {
                throw new ArgumentException("No category ID.", nameof(categoryId));
            }
            var coffeeItems = await _coffeeRepo.GetCoffeeItemsByCategoryIdAsync(categoryId);
          
            return coffeeItems.Select(c => new CoffeeResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                Size = c.Size,
                Stock = c.Stock,
                IsAvailable = c.IsAvailable,
                ImageUrl = c.ImageUrl,
                CategoryId = c.CategoryId,
                Category = new CoffeeCategoryDTO
                {
                    Id = c.Category?.Id ?? 0,
                    Name = c.Category?.Name ?? string.Empty,
                }
            });


        }

        public async Task<CoffeeResponse> UpdateCoffeeItemAsync(int id ,CoffeeRequest coffeeRequest)
        {
            if(id <= 0) 
                throw new ArgumentNullException("Invalid Id", nameof(id));
            var existingCoffee = await _coffeeRepo.GetCoffeeItemByIdAsync(id);
            if (existingCoffee is null)
            {
                throw new Exception("No coffee item found.");
            }

            existingCoffee.Name = coffeeRequest.Name;
            existingCoffee.Description = coffeeRequest.Description;
            existingCoffee.Price = coffeeRequest.Price;
            existingCoffee.Size = coffeeRequest.Size;
            existingCoffee.Stock = coffeeRequest.Stock;
            existingCoffee.IsAvailable = coffeeRequest.IsAvailable;
            existingCoffee.ImageUrl = coffeeRequest.ImageUrl;
            existingCoffee.CategoryId = coffeeRequest.CategoryId;


            await _coffeeRepo.UpdateCoffeeItemAsync(existingCoffee);
            
            return new CoffeeResponse
            {
                Id = existingCoffee.Id,
                Name = coffeeRequest.Name,
                Description = coffeeRequest.Description,
                Price = coffeeRequest.Price,
                Size = coffeeRequest.Size,
                Stock = coffeeRequest.Stock,
                IsAvailable = coffeeRequest.IsAvailable,
                ImageUrl = coffeeRequest.ImageUrl,
                CategoryId = existingCoffee.CategoryId,
                Category = new CoffeeCategoryDTO
                {
                    Id = existingCoffee.Category?.Id ?? 0,
                    Name = existingCoffee.Category?.Name ?? string.Empty,
                }
            };
        }
    }
}
