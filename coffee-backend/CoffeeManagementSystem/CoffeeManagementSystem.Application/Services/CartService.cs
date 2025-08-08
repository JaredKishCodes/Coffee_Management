

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
            var cart = await _cartRepo.GetOrCreateCartAsync();
            if (cart == null)
                throw new ArgumentException("Cart not found");

            foreach (var itemDto in addCartDto.CartItems)
            {
                var coffeeItem = await _coffeeItemRepo.GetCoffeeItemByIdAsync(itemDto.CoffeeItemId);
                if (coffeeItem == null)
                {
                    throw new Exception($"CoffeeItem with ID {itemDto.CoffeeItemId} not found.");
                }

                var existingItem =  cart.CartItems.FirstOrDefault(c => c.CoffeeItemId == itemDto.CoffeeItemId);
                if (existingItem != null) 
                {
                    existingItem.Quantity += itemDto.Quantity;
                    existingItem.Total = existingItem.Quantity * existingItem.UnitPrice;

                }

                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        CoffeeItemId = itemDto.CoffeeItemId,
                        Quantity = itemDto.Quantity,
                        CoffeeItem = coffeeItem,
                        UnitPrice = coffeeItem.Price,
                        Total = itemDto.Quantity * coffeeItem.Price
                    });
                }
            }

           

            cart.TotalPrice = cart.CartItems.Sum(i => i.Total);

            var result = await _cartRepo.UpdateCartAsync(cart);
            if (result == null)
            {
                return null;
            }

            return new CartDto
            {
                Id = result.Id,
                CustomerName = result.CustomerName,
                TotalPrice = result.CartItems.Sum(i => i.Total),
                CartItems = result.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeName = item.CoffeeItem.Name,
                    CoffeeItemId = item.CoffeeItemId,
                    CoffeeItemImg = item.CoffeeItem.ImageUrl,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,
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
                TotalPrice = cart.CartItems.Sum(i => i.Total),
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    CoffeeName = item.CoffeeItem.Name,
                    CoffeeItemImg = item.CoffeeItem.ImageUrl,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,
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
                TotalPrice = cart.CartItems.Sum(i => i.Total),
                CartItems = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeName = item.CoffeeItem.Name,
                    CoffeeItemId = item.CoffeeItemId,
                    CoffeeItemImg = item.CoffeeItem.ImageUrl,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,
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
                if(coffee == null)
                {
                    throw new ArgumentException("Coffee Id not found");
                }
                item.UnitPrice = coffee.Price;
                item.Total = item.UnitPrice * item.Quantity;
            }

            updatedCart.TotalPrice = updatedCart.CartItems.Sum(i => i.Total);


            var result = await _cartRepo.UpdateCartAsync(updatedCart);

            return new CartDto
            {
                Id = result.Id,
                CustomerName = result.CustomerName,
                TotalPrice = result.CartItems.Sum(i => i.Total),
                CartItems = result.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    CoffeeItemId = item.CoffeeItemId,
                    CoffeeName = item.CoffeeItem.Name,
                    CoffeeItemImg = item.CoffeeItem.ImageUrl,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,
                }).ToList()
            };
        }



    }
}
