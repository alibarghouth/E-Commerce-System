using AutoMapper;
using E_Commerce_System.DTO.OrderDto;
using E_Commerce_System.DTO.Response;
using E_Commerce_System.DTO.Response.Queries;
using E_Commerce_System.Model;
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

        private readonly IMapper _map;

        public OrdersController(IOrderService orderService, IMapper map)
        {
            _orderService = orderService;
            _map = map;
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetALLorderAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }
        
        [HttpGet("GetAllOrdersByfilter")]
        public async Task<IActionResult> GetAllOrdersByfilterAsync([FromQuery] string? orderId,[FromQuery] PaginationQueries queries)
        {
            if (queries.PageNumber < 1)
            {
                return BadRequest("no content in page 0");
            }

            var filter = _map.Map<PaginationFilter>(queries);
            var orders = await _orderService.GetAllOrdersByfilter(orderId,filter);



            var response = new PagedResponse<Order>(orders);
            response.PageSize = queries.PageSize;
            response.PageNumber = queries.PageNumber;
            response.NextPage = $"/api/Order/GetAllOrdersByfilter?PageNumber={response.PageNumber  +1}&PageSize={response.PageSize}";
            response.PreviousPage =response.PageNumber > 1? $"/api/Order/GetAllOrdersByfilter?PageNumber={response.PageNumber - 1}&PageSize={response.PageSize}" :null;

            return Ok(response);
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
