

using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Enums;
using CoffeeManagementSystem.Domain.Interfaces;

namespace CoffeeManagementSystem.Application.Services
{
    public class CartService(ICartRepo _cartRepo, ICoffeeItemRepo _coffeeItemRepo) : ICartService
    {
        public async Task<CartDto?> AddCartAsync(AddCartDto addCartDto)
        {
            var cart = new Cart
            {
                CustomerName = addCartDto.CustomerName,
                CartItems = new List<CartItem>()
            };

            foreach (var itemDto in addCartDto.CartItems)
            {
                var coffeeItem = await _coffeeItemRepo.GetCoffeeItemByIdAsync(itemDto.CoffeeItemId);
                if (coffeeItem == null)
                {
                    throw new Exception($"CoffeeItem with ID {itemDto.CoffeeItemId} not found.");
                }

                cart.CartItems.Add(new CartItem
                {
                    CoffeeItemId = itemDto.CoffeeItemId,
                    Quantity = itemDto.Quantity,
                    CoffeeItem = coffeeItem,
                    Total = itemDto.Quantity * coffeeItem.Price
                });
            }

            cart.TotalPrice = cart.CartItems.Sum(i => i.Total);

            var result = await _cartRepo.AddCartAsync(cart);
            if (result == null)
            {
                return null;
            }

            return new CartDto
            {
                Id = result.Id,
                CustomerName = result.CustomerName,
                TotalPrice = result.TotalPrice,
                CartItems = result.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    CoffeeItem = item.CoffeeItem?.Name ?? "Unknown"
                }).ToList()
            };
        }


        public async Task<bool> DeleteCartAsync(int id)
        {
            var toBeDeleted = await _cartRepo.GetCartByIdAsync(id);
            if (toBeDeleted == null)
                    throw new ArgumentException("Cart not found", nameof(id));

            return await _cartRepo.DeleteCartAsync(id);

        }

        public async Task<CartDto?> GetCartByIdAsync(int cartId)
        {
          var cart =   await _cartRepo.GetCartByIdAsync(cartId);
            if (cart == null)
            {
                return null;
            }
            return new CartDto
            {
                Id = cart.Id,
                CustomerName = cart.CustomerName,
                TotalPrice = cart.TotalPrice,
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    CoffeeItem = item.CoffeeItem?.Name ?? "Unknown"
                }).ToList()
            };
        }

        public async Task<IEnumerable<CartDto>> GetCartsAsync()
        {
           var carts = await _cartRepo.GetCartsAsync();

            return carts.Select(cart => new CartDto
            {
                Id = cart.Id,
                CustomerName = cart.CustomerName,
                TotalPrice = cart.TotalPrice,
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    CoffeeItem = item.CoffeeItem?.Name ?? "Unknown"
                }).ToList()
            });
        }

        public async Task<CartDto?> UpdateCartAsync(int id, AddCartDto addCartDto)
        {
            // Optional: Check if it exists first
            await _cartRepo.GetCartByIdAsync(id);

            // Build new Cart object
            var updatedCart = new Cart
            {
                Id = id,
                CustomerName = addCartDto.CustomerName,
                CartItems = addCartDto.CartItems.Select(item => new CartItem
                {
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Populate prices for each cart item
            foreach (var item in updatedCart.CartItems)
            {
                var coffee = await _coffeeItemRepo.GetCoffeeItemByIdAsync(item.CoffeeItemId);
                item.UnitPrice = coffee.Price;
                item.Total = item.UnitPrice * item.Quantity;
            }

            updatedCart.TotalPrice = updatedCart.CartItems.Sum(i => i.Total);


            var result = await _cartRepo.UpdateCartAsync(updatedCart);

            return new CartDto
            {
                Id = result.Id,
                CustomerName = result.CustomerName,
                TotalPrice = result.TotalPrice,
                CartItems = result.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,
                    CoffeeItem = item.CoffeeItem.Name
                }).ToList()
            };
        }



    }
}
