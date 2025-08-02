using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.Cart;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetAllCarts")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CartDto>>>> GetAllCartsAsync()
        {
            var carts = await _cartService.GetCartsAsync();
            if (carts == null || !carts.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CartDto>>
                {
                    Success = false,
                    Message = "Carts not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<IEnumerable<CartDto>>
            {
                Success = true,
                Message = "Carts fetched successfully!",
                Data = carts
            });
        }

        [HttpGet("GetCartById/{id}")]
        public async Task<ActionResult<ApiResponse<CartDto>>> GetCartById(int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound(new ApiResponse<CartDto>
                {
                    Success = false,
                    Message = $"Cart with ID {id} not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<CartDto>
            {
                Success = true,
                Message = "Cart fetched successfully",
                Data = cart
            });
        }

        [HttpPost("AddCart")]
        public async Task<ActionResult<ApiResponse<CartDto>>> AddCartAsync([FromBody] AddCartDto addCartDto)
        {
            var createdCart = await _cartService.AddCartAsync(addCartDto);
            if (createdCart == null)
            {
                return BadRequest(new ApiResponse<CartDto>
                {
                    Success = false,
                    Message = "Failed to create cart",
                    Data = null
                });
            }

            return CreatedAtAction(nameof(GetCartById), new { id = createdCart.Id }, new ApiResponse<CartDto>
            {
                Success = true,
                Message = "Cart created successfully",
                Data = createdCart
            });
        }

        [HttpPut("UpdateCart/{id}")]
        public async Task<ActionResult<ApiResponse<CartDto>>> UpdateCartAsync(int id, [FromBody] AddCartDto addCartDto)
        {
            var updatedCart = await _cartService.UpdateCartAsync(id, addCartDto);
            if (updatedCart == null)
            {
                return NotFound(new ApiResponse<CartDto>
                {
                    Success = false,
                    Message = "Cart not found or update failed",
                    Data = null
                });
            }

            return Ok(new ApiResponse<CartDto>
            {
                Success = true,
                Message = "Cart updated successfully",
                Data = updatedCart
            });
        }

        [HttpDelete("DeleteCart/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteCartAsync(int id)
        {
            var result = await _cartService.DeleteCartAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Cart not found or delete failed",
                    Data = null
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Cart deleted successfully",
                Data = null
            });
        }
    }
}
