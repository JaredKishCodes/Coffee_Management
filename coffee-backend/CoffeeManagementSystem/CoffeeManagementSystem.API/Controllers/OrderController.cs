using CoffeeManagementSystem.Application.DTOs;
using CoffeeManagementSystem.Application.DTOs.CoffeeInventory;
using CoffeeManagementSystem.Application.DTOs.Order;
using CoffeeManagementSystem.Application.Interfaces;
using CoffeeManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<ApiResponse<OrderDto>>>> GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound(
                    new ApiResponse<IEnumerable<OrderDto>>
                    {
                        Success = false,
                        Message = "Orders not found",
                        Data = null
                    });
            }
            return Ok(new ApiResponse<IEnumerable<OrderDto>>
            {
                Success = true,
                Message = "Orders fetched Successfully!",
                Data = orders
            });
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrderByIdAsync(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(
                    new ApiResponse<OrderDto>
                    {
                        Success = false,
                        Message = "Order not found",
                        Data = null
                    });
            }
            return Ok(new ApiResponse<OrderDto>
            {
                Success = true,
                Message = "Orders fetched Successfully!",
                Data = order
            });
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> AddOrderAsync([FromBody] CreateOrderDto createOrderDto)
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

            var newOrder = await _orderService.CreateOrderAsync(createOrderDto);
            if (newOrder is null)
            {
                return NotFound(new ApiResponse<OrderDto>
                {
                    Success = false,
                    Message = $"No Order found.",
                    Data = null
                });
            }
            return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = newOrder.Id },
                new ApiResponse<OrderDto>
                {
                    Success = true, 
                    Message = "Order created successfully!",
                    Data = newOrder
                });

        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> UpdateOrderAsync(int id, [FromBody] CreateOrderDto updateOrderDto)
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

            var updatedOrder = await _orderService.UpdateOrderAsync(id , updateOrderDto);
            if (updatedOrder is null)
            {
                return NotFound(new ApiResponse<OrderDto>
                {
                    Success = false,
                    Message = $"No Order found.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<OrderDto>
            {
                Success = true,
                Message = "Order updated successfully!",
                Data = updatedOrder
            });
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> DeleteOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(
                    new ApiResponse<IEnumerable<OrderDto>>
                    {
                        Success = false,
                        Message = "Order not found",
                        Data = null
                    });
            }
            var deletedOrder = await _orderService.DeleteOrderAsync(order.Id);
            return Ok(new ApiResponse<OrderDto>
            {
                Success = true,
                Message = "Order Deleted Successfully!",
                Data = null
            });
        }


    }
}
