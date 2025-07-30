

using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CoffeeRepo(CoffeeDbContext _context) : ICoffeeItemRepo
    {
        public async Task<CoffeeItem> AddCoffeeItemAsync(CoffeeItem coffeeItem)
        {
           await _context.CoffeeItems.AddAsync(coffeeItem);
           await _context.SaveChangesAsync();
            var addedItem = await _context.CoffeeItems
                .Include(c => c.Category)
                .FirstOrDefaultAsync(ci => ci.Id == coffeeItem.Id);
            return addedItem ?? coffeeItem; // Return the original item if query fails
        }

        public async Task<bool> DeleteCoffeeItemAsync(int id)
        {
           var coffee = await _context.CoffeeItems.FirstOrDefaultAsync(ci => ci.Id == id);
            if(coffee is not null)
            {
                _context.CoffeeItems.Remove(coffee);
               await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CoffeeItem>> GetAllCoffeeItemsAsync()
        {
          return await _context.CoffeeItems.Include(c => c.Category).ToListAsync();
        }

        public async Task<CoffeeItem?> GetCoffeeItemByIdAsync(int id)
        {
            var coffee = await _context.CoffeeItems.FirstOrDefaultAsync(ci => ci.Id == id);
            if(coffee is not null)
            {
                return await _context.CoffeeItems
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(ci => ci.Id == id);
            }
            return null;
        }

        public async Task<IEnumerable<CoffeeItem>> GetCoffeeItemsByCategoryIdAsync(int categoryId)
        {
            var category = await _context.CoffeeCategories
                .Include(c => c.CoffeeItems)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            return category?.CoffeeItems ?? new List<CoffeeItem>();

        }

        public async Task<CoffeeItem> UpdateCoffeeItemAsync(CoffeeItem coffeeItem)
        {
            var existingCoffeeItem = await _context.CoffeeItems.FirstOrDefaultAsync(ci => ci.Id == coffeeItem.Id);
            if (existingCoffeeItem is not null)
            {
                existingCoffeeItem.Name = coffeeItem.Name;
                existingCoffeeItem.Description = coffeeItem.Description;
                existingCoffeeItem.Price = coffeeItem.Price;
                existingCoffeeItem.CategoryId = coffeeItem.CategoryId;
                
                await  _context.SaveChangesAsync();

                return existingCoffeeItem;
            }
            return coffeeItem; // Return the input item if not found
        }
    }
}
