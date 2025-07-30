

using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using CoffeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementSystem.Infrastructure.Repositories
{
    public class OrderRepo(CoffeeDbContext _context) : IOrderRepo
    {
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order; 
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order is null)
                return false;

             _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
             .Include(o => o.OrderItems)                      
             .ThenInclude(oi => oi.CoffeeItem)               
             .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.CoffeeItem)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
            if (existingOrder is null)
                throw new ArgumentException("Id not found",nameof(existingOrder));
            
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.OrderStatus = order.OrderStatus;          

            await _context.SaveChangesAsync();

            return existingOrder;

        }
    }
}
