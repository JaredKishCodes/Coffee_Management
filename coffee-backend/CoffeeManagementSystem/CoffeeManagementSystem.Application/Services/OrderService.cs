
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Enums;
using CoffeeManagementSystem.Domain.Interfaces;

namespace CoffeeManagementSystem.Application.Services
{
    public class OrderService(IOrderRepo _orderRepo,ICoffeeItemRepo _coffeeItemRepo) : IOrderService
    {
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto order)
        {
            var newOrder = new Order
            {
                CustomerName = order.CustomerName,
                OrderItems = order.OrderItems.Select(item => new OrderItem
                {
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                   
                }).ToList()
            };

            foreach (var item in newOrder.OrderItems)
            {
                var coffee = await _coffeeItemRepo.GetCoffeeItemByIdAsync(item.CoffeeItemId);
                item.UnitPrice = coffee.Price;
            }

            newOrder.TotalPrice = newOrder.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            newOrder.OrderDate = DateTime.UtcNow;
            newOrder.OrderStatus = OrderStatus.Pending;

            var savedOrder = await _orderRepo.CreateOrderAsync(newOrder); // returns Order entity

            return new OrderDto
            {
                Id = savedOrder.Id,
                CustomerName = savedOrder.CustomerName,
                TotalPrice = savedOrder.TotalPrice,
                OrderDate = savedOrder.OrderDate,
                OrderStatus = savedOrder.OrderStatus,
                OrderItems = savedOrder.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CoffeeName = item.CoffeeItem?.Name ?? "Unknown"
                    
                }).ToList()
            };
        }


        public async Task<bool> DeleteOrderAsync(int id)
        {
          return await _orderRepo.DeleteOrderAsync(id);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CoffeeName = item.CoffeeItem?.Name ?? "Unknown"
                }).ToList()
            });
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CoffeeName = item.CoffeeItem?.Name ?? "Unknown"
                }).ToList()
            };
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, CreateOrderDto order)
        {
            await _orderRepo.GetOrderByIdAsync(id);
            var existingOrder = new Order
            {
                Id = id,
                CustomerName = order.CustomerName,
                OrderItems = order.OrderItems.Select(item => new OrderItem
                {
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                }).ToList()
            };
            foreach (var item in existingOrder.OrderItems)
            {
                var coffee = await _coffeeItemRepo.GetCoffeeItemByIdAsync(item.CoffeeItemId);
                item.UnitPrice = coffee.Price;
            }

            existingOrder.TotalPrice = existingOrder.OrderItems.Sum(i => i.Quantity * i.UnitPrice);
            existingOrder.OrderDate = DateTime.UtcNow;
            existingOrder.OrderStatus = OrderStatus.Pending;
            var updatedOrder = await _orderRepo.UpdateOrderAsync(existingOrder);

            return new OrderDto
            {
                Id = updatedOrder.Id,
                CustomerName = updatedOrder.CustomerName,
                TotalPrice = updatedOrder.TotalPrice,
                OrderDate = updatedOrder.OrderDate,
                OrderStatus = updatedOrder.OrderStatus,
                OrderItems = updatedOrder.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CoffeeName = item.CoffeeItem.Name,
                }).ToList()
            };
            
        }
    }
}
