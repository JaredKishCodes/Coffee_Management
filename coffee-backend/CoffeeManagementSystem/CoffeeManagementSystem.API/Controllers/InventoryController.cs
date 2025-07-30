using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.Coffee;
using CoffeeManagementSystem.Application.DTOs.CoffeeInventory;
using CoffeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController: ControllerBase
    {
        private readonly ICoffeeInventoryService _coffeeInventoryService;

        public InventoryController(ICoffeeInventoryService coffeeInventoryService)
        {
            _coffeeInventoryService = coffeeInventoryService;
        }
        [HttpGet("GetInventoryByCategory")] 
        public async Task<ActionResult<IEnumerable<ApiResponse<CoffeeInventoryDto>>>> GetInventoryByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest(new ApiResponse<IEnumerable<CoffeeInventoryDto>>
                {
                    Success = false,
                    Message = "Invalid category ID.",
                    Data = null
                });
            }
            var inventory = await _coffeeInventoryService.GetInventoryByCategoryAsync(categoryId);
            if (inventory is null || !inventory.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CoffeeInventoryDto>>
                {
                    Success = false,
                    Message = $"No inventory found for category ID {categoryId}.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<IEnumerable<CoffeeInventoryDto>>
            {
                Success = true,
                Message = "Inventory fetched successfully!",
                Data = inventory
            });
        }

        [HttpPut("UpdateStockAsync/{id}")]
        public async Task<ActionResult<ApiResponse<CoffeeInventoryDto>>> UpdateStockAsync(int id,[FromBody] UpdateCoffeeStockRequest updateCoffeeStockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CoffeeInventoryDto>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            };
          var updatedInventory =  await _coffeeInventoryService.UpdateStockAsync(id, updateCoffeeStockRequest);
            if (updatedInventory is null)
            {
                return NotFound(new ApiResponse<CoffeeInventoryDto>
                {
                    Success = false,
                    Message = $"No inventory found with ID {id}.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<CoffeeInventoryDto>
            {
                Success = true,
                Message = "Stock updated successfully!",
                Data = updatedInventory
            });
        }

        [HttpGet("GetInventoryById/{id}")]
        public async Task<ActionResult<ApiResponse<CoffeeInventoryDto>>> GetInventoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<CoffeeInventoryDto>
                {
                    Success = false,
                    Message = "Invalid item ID.",
                    Data = null
                });
            }
            var inventory = await _coffeeInventoryService.GetInventoryByIdAsync(id);
            if (inventory is null)
            {
                return NotFound(new ApiResponse<CoffeeInventoryDto>
                {
                    Success = false,
                    Message = $"No inventory found with ID {id}.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<CoffeeInventoryDto>
            {
                Success = true,
                Message = "Inventory fetched successfully!",
                Data = inventory
            });
        }

        [HttpGet("GetAllInventory")]
        public async Task<ActionResult<ApiResponse<CoffeeInventoryDto>>> GetAllInventoryAsync()
        {
            var inventory = await _coffeeInventoryService.GetAllInventoryAsync();
            if (inventory is null || !inventory.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CoffeeInventoryDto>>
                {
                    Success = false,
                    Message = "No inventory found.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<IEnumerable<CoffeeInventoryDto>>
            {
                Success = true,
                Message = "All inventory fetched successfully!",
                Data = inventory
            });
        }



    }
}
