using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("GetCartItems")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CartItemDto>>>> GetCartItemsAsync()
        {
            var cartItems = await _cartItemService.GetCartItemsAsync();
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CartItemDto>>
                {
                    Success = false,
                    Message = "Cart is empty",
                    Data = null
                });
            }
            return Ok(new ApiResponse<IEnumerable<CartItemDto>>
            {
                Success = true,
                Message = "Cart items fetched successfully!",
                Data = cartItems
            });
        }

        [HttpGet("GetCartItemById/{id}")]
        public async Task<ActionResult<ApiResponse<CartItemDto>>> GetCartItemByIdAsync(int id)
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound(new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Cart item not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<CartItemDto>
            {
                Success = true,
                Message = "Cart item fetched successfully!",
                Data = cartItem
            });
        }

        [HttpPost("AddCartItem")]
        public async Task<ActionResult<ApiResponse<CartItemDto>>> AddCartItem([FromBody] AddCartItemDto addCartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<OrderDto>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var addedCartItem = await _cartItemService.AddCartItemAsync(addCartItemDto);
            if (addedCartItem == null)
            {
                return BadRequest(new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Failed to add cart item",
                    Data = null
                });
            }

            return CreatedAtAction(nameof(GetCartItemByIdAsync), new { id = addedCartItem.Id }, new ApiResponse<CartItemDto>
            {
                Success = true,
                Message = "Cart item added successfully!",
                Data = addedCartItem
            });
        }

        [HttpPut("UpdateCartItem/{id}")]
        public async Task<ActionResult<ApiResponse<CartItemDto>>> UpdateCartItem([FromBody] int id, int quantity)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Failed to add cart item",
                    Data = null
                });
            }

            var updatedCartItem = await _cartItemService.UpdateCartItemAsync(id, quantity);
            if (updatedCartItem == null)
            {
                return NotFound(new ApiResponse<CartItemDto>
                {
                    Success = false,
                    Message = "Cart item not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<CartItemDto>
            {
                Success = true,
                Message = "Cart item fetched successfully!",
                Data = updatedCartItem
            });

        }
    }

    
 }
