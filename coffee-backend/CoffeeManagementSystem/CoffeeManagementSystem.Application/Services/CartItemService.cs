

using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoffeeManagementSystem.Application.Services
{
    public class CartItemService(ICartItemRepo _cartItemRepo,ICoffeeItemRepo _coffeeItemRepo
                                ,ICartRepo _cartRepo, ILogger<CartItemService> logger) : ICartItemService
    {
        public async Task<CartItemDto?> AddCartItemAsync(AddCartItemDto cartItemDto)
        {
            var cart = await _cartRepo.GetCartByIdAsync(cartItemDto.CartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var coffee = await _coffeeItemRepo.GetCoffeeItemByIdAsync(cartItemDto.CoffeeItemId);
            if (coffee == null)
                throw new Exception("Coffee not found");

            if (coffee.Stock < cartItemDto.Quantity)
                throw new Exception("Insufficient stock");

            // Check if cart item already exists for this coffee in this cart
            var existingCartItems = await _cartItemRepo.GetCartItemsAsync();
            var existingCartItem = existingCartItems.FirstOrDefault(ci => 
                ci.CartId == cartItemDto.CartId && ci.CoffeeItemId == cartItemDto.CoffeeItemId);

            if (existingCartItem != null)
            {
                // Update existing cart item quantity
                existingCartItem.Quantity += cartItemDto.Quantity;
                existingCartItem.Total = existingCartItem.UnitPrice * existingCartItem.Quantity;
                
                var updatedItem = await _cartItemRepo.UpdateCartItemAsync(existingCartItem.Id, existingCartItem.Quantity);
                if (updatedItem == null) return null;

                return new CartItemDto
                {
                    Id = updatedItem.Id,
                    CoffeeItemId = updatedItem.CoffeeItem.Id,
                    CoffeeItemImg = updatedItem.CoffeeItem.ImageUrl,
                    Quantity = updatedItem.Quantity,
                    UnitPrice = updatedItem.UnitPrice,
                    Total = updatedItem.UnitPrice * updatedItem.Quantity
                };
            }

            logger.LogInformation($"Adding new cart item - CoffeeItemId: {cartItemDto.CoffeeItemId}, Quantity: {cartItemDto.Quantity}, CartId: {cartItemDto.CartId}");
            Console.WriteLine($"CoffeeItemId: {cartItemDto.CoffeeItemId}, Quantity: {cartItemDto.Quantity}, CartId: {cartItemDto.CartId}");
            Console.WriteLine($"Coffee found: {coffee?.Name ?? "NULL"}");
            logger.LogInformation($"Coffee found: {coffee?.Name}");

            var cartItemEntity = new CartItem
            {
                CoffeeItemId = cartItemDto.CoffeeItemId,
                Quantity = cartItemDto.Quantity,
                CartId = cartItemDto.CartId,
                UnitPrice = coffee.Price,
                Total = coffee.Price * cartItemDto.Quantity
            };

            coffee.Stock -= cartItemDto.Quantity;
            await _coffeeItemRepo.UpdateCoffeeItemAsync(coffee);

            var addedItem = await _cartItemRepo.AddCartItemAsync(cartItemEntity);
            if (addedItem == null) return null;
            Console.WriteLine($"Coffee Item URl found: {coffee.ImageUrl ?? "NULL"}");

            return new CartItemDto
            {
                Id = addedItem.Id,
                CoffeeItemImg = addedItem.CoffeeItem.ImageUrl,
                CoffeeItemId = addedItem.CoffeeItem.Id,
                Quantity = addedItem.Quantity,
                UnitPrice = addedItem.UnitPrice,
                Total = coffee.Price * addedItem.Quantity
            };
        }

        public async Task<bool> DeleteCartItemAsync(int cartItemId)
        {
         var toBeDeleted =  await  _cartItemRepo.GetCartItemByIdAsync(cartItemId);
            if (toBeDeleted == null)
            {
                throw new Exception("Cart item not found");
            }
            return await _cartItemRepo.DeleteCartItemAsync(cartItemId);

        }

        public async Task<CartItemDto?> GetCartItemByIdAsync(int cartItemId)
        {
            var cartItem = await _cartItemRepo.GetCartItemByIdAsync(cartItemId);

            if (cartItem == null) return null;

            return new CartItemDto
            {
                Id = cartItem.Id,
                CoffeeItemId = cartItem.CoffeeItem.Id,
                CoffeeItemImg = cartItem.CoffeeItem.ImageUrl,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice,
                Total = cartItem.UnitPrice * cartItem.Quantity
            };
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync()
        {
            var cartItems = await _cartItemRepo.GetCartItemsAsync();

            return cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                CoffeeItemId = item.CoffeeItem.Id,
                CoffeeItemImg = item.CoffeeItem.ImageUrl,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = item.UnitPrice * item.Quantity
            });

        }

        public async Task<CartItemDto?> UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepo.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }

            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero");
            }


            var updatedItem = await _cartItemRepo.UpdateCartItemAsync(cartItemId, quantity);

            if (updatedItem == null) return null;

            return new CartItemDto
            {
                Id = updatedItem.Id,
                CoffeeItemId = updatedItem.CoffeeItem.Id,
                CoffeeItemImg = updatedItem.CoffeeItem.ImageUrl,
                Quantity = updatedItem.Quantity,
                UnitPrice = updatedItem.UnitPrice,
                Total = updatedItem.UnitPrice * updatedItem.Quantity
            };
        }

    }
}
