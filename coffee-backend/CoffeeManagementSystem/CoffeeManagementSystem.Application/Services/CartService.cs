

using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using CoffeeManagementSystem.Domain.Interfaces;

namespace CoffeeManagementSystem.Application.Services
{
    public class CartService(ICartItemRepo _cartItemRepo,ICoffeeItemRepo _coffeeItemRepo) : ICartItemService
    {
        public async Task<CartItemDto?> AddCartItemAsync(AddCartItemDto cartItem)
        {
           var  coffee = await _coffeeItemRepo.GetCoffeeItemByIdAsync(cartItem.CoffeeItemId);
            if (coffee == null)
                throw new Exception("Coffee not found");

            if (coffee.Stock < cartItem.Quantity)
                throw new Exception("Insufficient stock");


            var cartItemEntity = new CartItem
            {
                CoffeeItemId = cartItem.CoffeeItemId,
                Quantity = cartItem.Quantity,
            };

            coffee.Stock -= cartItem.Quantity;
            await _coffeeItemRepo.UpdateCoffeeItemAsync(coffee);

            var addedItem = await _cartItemRepo.AddCartItemAsync(cartItemEntity);
            if (addedItem == null) return null;

            return new CartItemDto
            {
                Id = addedItem.Id,
                CoffeeItemId = addedItem.CoffeeItemId,
                Quantity = addedItem.Quantity,
                UnitPrice = coffee.Price,
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
                CoffeeItemId = cartItem.CoffeeItemId,
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
                CoffeeItemId = item.CoffeeItemId,
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
                CoffeeItemId = updatedItem.CoffeeItemId,
                Quantity = updatedItem.Quantity,
                UnitPrice = updatedItem.UnitPrice,
                Total = updatedItem.UnitPrice * updatedItem.Quantity
            };
        }

    }
}
