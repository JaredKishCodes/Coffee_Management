using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.Coffee;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }
        [Authorize(Roles = "Admin,Customer,Staff")]
        [HttpGet("GetAllCoffees")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CoffeeResponse>>>> GetAllCoffeeItems()
        {
            var items = await _coffeeService.GetAllCoffeeItemsAsync();

            if(items == null || !items.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CoffeeResponse>>
                {
                    Success = false,
                    Message = "Coffee items not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponse<IEnumerable<CoffeeResponse>>
            {
                Success = true,
                Message = "Coffee items retrieved successfully.",
                Data = items
            });
        }
        [Authorize(Roles = "Admin,Customer,Staff")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CoffeeResponse>>> GetCoffeeItemById(int id)
        {
            var coffeeItem = await _coffeeService.GetCoffeeItemByIdAsync(id);
            if (coffeeItem is null)
            {
                return NotFound("Coffee item is null");
            }
            return Ok(new ApiResponse<CoffeeResponse>
            {
                Success = true,
                Message = "Coffee item retrieved successfully.",
                Data = coffeeItem,
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CoffeeResponse>>> AddCoffeeItem([FromBody] CoffeeRequest coffeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CoffeeResponse>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }
            var newCoffee = await _coffeeService.AddCoffeeItemAsync(coffeeRequest);

            return CreatedAtAction(nameof(GetCoffeeItemById), new { id = newCoffee.Id },
                new ApiResponse<CoffeeResponse>
                {
                    Success = true,
                    Message = "Coffee item created successfully",
                    Data = newCoffee,
                });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CoffeeResponse>>> UpdateCoffeeItem(int id, [FromBody] CoffeeRequest coffeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CoffeeResponse>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var updatedCoffee = await _coffeeService.UpdateCoffeeItemAsync(id, coffeeRequest);
            if (updatedCoffee is null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<CoffeeResponse>
            {
                Success = true,
                Message = "Coffee item updated successfully",
                Data = updatedCoffee,
            });
        }
        [Authorize(Roles = "Admin,Customer,Staff")]
        [HttpGet("GetCoffeesByCategory")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CoffeeResponse>>>> GetCoffeeItemsByCategory(int categoryId)
        {
            var coffees = await _coffeeService.GetCoffeeItemsByCategoryIdAsync(categoryId);

            if (coffees is null || !coffees.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CoffeeResponse>>
                {
                    Success = false,
                    Message = "Coffee items not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponse<IEnumerable<CoffeeResponse>>
            {
                Success = true,
                Message = "Coffee items by category retrieved successfully.",
                Data = coffees,
            });

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<CoffeeResponse>>> DeleteCoffeeItem(int id)
        {
            var result = await _coffeeService.DeleteCoffeeItemAsync(id);
            if (!result)
            {
                return NotFound("Coffee id not found");
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Coffee item deleted successfully.",
                Data = null
            });
        }
    }
}
