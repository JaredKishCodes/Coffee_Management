
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class OrderItemRepo(CoffeeDbContext _context) : IOrderItemRepo
    {
        public async Task<OrderItem?> CreateOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;

        }

        public async Task<bool> DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
           return await _context.OrderItems.Include(c => c.CoffeeItem).ToListAsync();
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _context.OrderItems
                .Include(c => c.CoffeeItem)
                .AsNoTracking()
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);
        }

        public async Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem)
        {
            var existingOrderItem = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItem.Id);
            if (existingOrderItem is null)
                return null;

            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.UnitPrice = orderItem.UnitPrice;
            existingOrderItem.Total = orderItem.Total;

            await _context.SaveChangesAsync();

            return existingOrderItem;
        }
    }    
    }
