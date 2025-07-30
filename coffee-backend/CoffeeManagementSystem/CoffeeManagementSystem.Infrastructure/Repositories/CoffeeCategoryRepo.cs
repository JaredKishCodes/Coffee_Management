

using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CoffeeCategoryRepo(CoffeeDbContext _context) : ICoffeeCategoryRepo
    {
        public async Task<CoffeeCategory> AddCoffeeCategoryAsync(CoffeeCategory coffeeCategory)
        {
           await _context.CoffeeCategories.AddAsync(coffeeCategory);
           await _context.SaveChangesAsync();

           return coffeeCategory;
        }

        public async Task<bool> DeleteCoffeeCategoryAsync(int id)
        {
            var coffee =  await _context.CoffeeCategories.FirstOrDefaultAsync(ci => ci.Id == id);
            if (coffee is not null)
            {
                _context.CoffeeCategories.Remove(coffee);
               await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CoffeeCategory>> GetAllCoffeeCategoriesAsync()
        {
          return await  _context.CoffeeCategories.Include(c => c.CoffeeItems).ToListAsync();
        }

        public async Task<CoffeeCategory?> GetCoffeeCategoryByIdAsync(int id)
        {
            var coffee = await _context.CoffeeCategories
                .Include(c => c.CoffeeItems)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            return coffee;
        }

        public async Task<CoffeeCategory> UpdateCoffeeCategoryAsync(CoffeeCategory coffeeCategory)
        {
            var existingCategory = await _context.CoffeeCategories
                .FirstOrDefaultAsync(c => c.Id == coffeeCategory.Id);

            if (existingCategory is not null)
            {
                existingCategory.Name = coffeeCategory.Name;
                existingCategory.Description = coffeeCategory.Description;            
                
              await  _context.SaveChangesAsync();
                return existingCategory;
            }
            return coffeeCategory; // Return the input category if not found
        }
    }
}
