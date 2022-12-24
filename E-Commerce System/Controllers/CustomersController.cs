using E_Commerce_System.DTO.CustomerDto;
using E_Commerce_System.Model;
using E_Commerce_System.Service.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUser user)
        {
            var users = await _service.RegisterUser(user);

            return Ok(users);
        }



        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginUser user)
        {
            var users = await _service.LoginUser(user);

            if (!ModelState.IsValid)
            {
                return BadRequest("Error");
            }

            if (!users.IsAuthenticated)
            {
                return BadRequest("Error");
            }

            return Ok(users);
        }

        [HttpPut("LogOut")]
        public async Task<IActionResult> LogOut (int id)
        {
            var customer = await _service.LogOut(id);

            return Ok(customer);
        }

        [HttpGet("GetAllUser"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var customer = await _service.GetAllCustomer();

            return Ok(customer);
        }
    }
}
