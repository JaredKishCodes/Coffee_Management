

using CoffeeManagementSystem.Application.DTOs.CoffeeInventory;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Interfaces;

namespace CoffeeManagementSystem.Application.Services
{
    public class CoffeeInventoryService(ICoffeeInventoryRepo _coffeeInventoryRepo) : ICoffeeInventoryService
    {
        public async Task<IEnumerable<CoffeeInventoryDto>> GetAllInventoryAsync()
        {
            var items = await _coffeeInventoryRepo.GetAllInventoryAsync();

            return items.Select(x => new CoffeeInventoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Stock = x.Stock,
                IsAvailable = x.IsAvailable
            });
        }

        public async Task<IEnumerable<CoffeeInventoryDto>> GetInventoryByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category ID must be greater than zero.", nameof(categoryId));

            var items = await _coffeeInventoryRepo.GetInventoryByCategoryAsync(categoryId);

            if (items is null)
            {
                throw new KeyNotFoundException($"No inventory found for category ID {categoryId}.");
            }

            return items.Select(x => new CoffeeInventoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Stock = x.Stock,
                IsAvailable = x.IsAvailable
            });
        }
            
        public async Task<CoffeeInventoryDto?> GetInventoryByIdAsync(int itemId)
        {
            if (itemId <= 0)
                throw new ArgumentException("Item ID must be greater than zero.", nameof(itemId));

            var inventory = await _coffeeInventoryRepo.GetInventoryByIdAsync(itemId);
            if (inventory is null)
            {
                throw new KeyNotFoundException($"No inventory found");
            }
            return new CoffeeInventoryDto
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Stock = inventory.Stock,
                IsAvailable = inventory.IsAvailable
            };


        }

        public async Task<CoffeeInventoryDto?> UpdateStockAsync(int id, UpdateCoffeeStockRequest updateCoffeeStockRequest)
        {
            if (id <= 0)
                throw new ArgumentException("Item ID must be greater than zero.", nameof(id));

            var existingItem = await _coffeeInventoryRepo.GetInventoryByIdAsync(id);

            if (existingItem is null) { return null; }

            if (updateCoffeeStockRequest.Stock < 0)
            {
                throw new ArgumentException("New stock cannot be negative.", nameof(updateCoffeeStockRequest.Stock));
            }

            var updatedItem = await _coffeeInventoryRepo.UpdateStockAsync(id, updateCoffeeStockRequest.Stock);

            if (updatedItem == null)
                return null;

            return new CoffeeInventoryDto
            {
                Id = updatedItem.Id,
                Name = updatedItem.Name,
                Stock = updatedItem.Stock,
                IsAvailable = updatedItem.IsAvailable
            };

        }
    }
}
