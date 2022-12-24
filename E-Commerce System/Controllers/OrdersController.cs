using E_Commerce_System.DTO.OrderDto;
using E_Commerce_System.Service.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetALLorderAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);

            return Ok(order);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(RegisterOrder order)
        {
            var orders = await _orderService.AddOrderAsync(order);

            return Ok(orders);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.DeleteOrder(id);

            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder (int id, UpdateOrder order)
        {
            var orders = await _orderService.UpdateOrder(id,order); 

            return Ok(orders);
        }

        
    }
}
