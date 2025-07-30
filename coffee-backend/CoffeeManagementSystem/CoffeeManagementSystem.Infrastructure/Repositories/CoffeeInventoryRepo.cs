

using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class CoffeeInventoryRepo(CoffeeDbContext _context) : ICoffeeInventoryRepo
    {
        public async Task<IEnumerable<CoffeeItem>> GetAllInventoryAsync()
        {
           return await _context.CoffeeItems.ToListAsync();
        }

        public async Task<IEnumerable<CoffeeItem>> GetInventoryByCategoryAsync(int categoryId)
        {
            return await _context.CoffeeItems.Where(c => c.CategoryId == categoryId).ToListAsync();

        }

        public async Task<CoffeeItem?> GetInventoryByIdAsync(int itemId)
        {
            return await _context.CoffeeItems.FirstOrDefaultAsync(ci => ci.Id == itemId);
        }

        public async Task<CoffeeItem?> UpdateStockAsync(int id, int newStock)
        {
            var item = await _context.CoffeeItems
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (item == null)
            {
                return null;
            }

            item.Stock = newStock;
            item.IsAvailable = newStock > 0;

            _context.CoffeeItems.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
