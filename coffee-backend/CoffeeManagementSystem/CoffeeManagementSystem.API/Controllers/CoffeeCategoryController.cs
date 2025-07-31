using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.Coffee;
using CoffeeManagementSystem.Application.DTOs.CoffeeCategory;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeCategoryController : ControllerBase
    {
        private readonly ICoffeeCategoryService _coffeeCategoryService;

        public ILogger<CoffeeCategoryController> _logger { get; }

        public CoffeeCategoryController(ICoffeeCategoryService coffeeCategoryService,ILogger<CoffeeCategoryController> logger)
        {
            _coffeeCategoryService = coffeeCategoryService;
            _logger = logger;
        }

        [HttpGet("GetAllCoffeeCategories")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AllCategoriesDto>>>> GetAllCoffeeCategoriesAsync()
        {
            _logger.LogInformation("Fetching all coffee categories.");
            var categories = await _coffeeCategoryService.GetAllCoffeeCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<AllCategoriesDto>>
                {
                    Success = false,
                    Message = "Coffee categories not found.",
                    Data = null,
                });
            }
            return Ok(new ApiResponse<IEnumerable<AllCategoriesDto>>
            {
                Success = true,
                Message = "Coffee Categories fetched successfully!",
                Data = categories
            });
        }
        [Authorize(Roles = "Admin,Customer,Staff")]
        [HttpGet("GetCoffeeCategoryById/{id}")]
        public async Task<ActionResult<ApiResponse<AllCategoriesDto>>> GetCoffeeCategoryByIdAsync(int id)
        {

            var category = await _coffeeCategoryService.GetCoffeeCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new ApiResponse<AllCategoriesDto>
                {
                    Success = false,
                    Message = "Coffee category not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<AllCategoriesDto>
            {
                Success = true,
                Message = "Coffee category fetched successfully!",
                Data = category
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddCoffeeCategory")]
        public async Task<ActionResult<ApiResponse<AllCategoriesDto>>> AddCoffeeCategoryAsync([FromBody] CoffeeCategoryReq coffeeCategoryReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<AllCategoriesDto>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var newCoffeeCategory = await _coffeeCategoryService.AddCoffeeCategoryAsync(coffeeCategoryReq);

            return CreatedAtRoute("GetCoffeeCategoryById",
                new { id = newCoffeeCategory.Id },
                new ApiResponse<AllCategoriesDto>
                {
                    Success = true,
                    Message = "Coffee category created successfully",
                    Data = newCoffeeCategory,
                });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCoffeeCategory/{id}")]
        public async Task<ActionResult<ApiResponse<AllCategoriesDto>>> UpdateCoffeeCategoryAsync(int id, [FromBody] CoffeeCategoryReq coffeeCategoryReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<AllCategoriesDto>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            try
            {
                var updatedCategory = await _coffeeCategoryService.UpdateCoffeeCategoryAsync(id, coffeeCategoryReq);

                return Ok(new ApiResponse<AllCategoriesDto>
                {
                    Success = true,
                    Message = "Coffee category updated successfully",
                    Data = updatedCategory,
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new ApiResponse<AllCategoriesDto>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCoffeeCategory/{id}")]
        public async Task<ActionResult<ApiResponse<AllCategoriesDto>>> DeleteCoffeeCategoryAsync(int id)
        {
            var result = await _coffeeCategoryService.DeleteCoffeeCategoryAsync(id);
            if (!result)
            {
                return NotFound("Coffee category id not found");
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Coffee category item deleted successfully.",
                Data = null
            });
        }
    }
}
