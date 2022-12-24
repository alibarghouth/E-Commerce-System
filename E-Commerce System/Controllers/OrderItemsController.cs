using E_Commerce_System.DTO.OrderItemDto;
using E_Commerce_System.Service.OrderItemService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("GetAllOrderItem")]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrders();

            return Ok(orderItems);
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItems(RegisterOrderItem orderItem) 
        {
            var orderItems = await _orderItemService.AddOrderItem(orderItem);

            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemsByID(int id)
        {
            var orderItems = await _orderItemService.GetOrderItemById(id);

            return Ok(orderItems);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItems(int id, UpdatedOrderItem orderItem)
        {
            var orderItems =await _orderItemService.UpdateOrderItem(id, orderItem);

            return Ok(orderItems);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItems(int id)
        {
            var orderItems =await _orderItemService.DeleteOrderItem(id);

            return Ok(orderItems);
        }
    }
}
