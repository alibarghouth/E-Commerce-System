using AutoMapper;
using E_Commerce_System.DTO.CustomerDto;
using E_Commerce_System.DTO.Response;
using E_Commerce_System.DTO.Response.Queries;
using E_Commerce_System.Model;
using E_Commerce_System.Service.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        private readonly IConfiguration _configuration;

        private readonly IMapper _map;
        
        public CustomersController(ICustomerService service, IConfiguration configuration, IMapper map)
        {
            _service = service;
            _configuration = configuration;
            _map = map;
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
            var customer =  await _service.GetCustomerByUserName(user);

            if (!ModelState.IsValid)
            {
                return BadRequest("Error");
            }

            if (!users.IsAuthenticated)
            {
                return BadRequest("Error");
            }


            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, customer);

            return Ok(users);
        }

        [HttpPut("LogOut")]
        public async Task<IActionResult> LogOut (int id)
        {
            var customer = await _service.LogOut(id);

            return Ok(customer);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser([FromQuery] FilterQuery? query,[FromQuery] PaginationQueries queries)
        {
            var paginationFilter = _map.Map<PaginationFilter>(queries);

            var customer = await _service.GetAllCustomer(query.UserId,paginationFilter);

            var paginationResponse = new PagedResponse<Customer>(customer);
            return Ok(paginationResponse);
        }

        [HttpGet("GetAllOrderItem")]
        public async Task<IActionResult> GetAllOrderForCustomer()
        {
            var orderItem = _service.GetOrderItemByCoustomerId();

            return Ok(orderItem);
        }

        [HttpPost("refresh-token"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefreshToken(Customer customer)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!customer.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (customer.TokenExpier < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var token =  CreateToken(customer);
            string Createdtoken =new JwtSecurityTokenHandler().WriteToken(token);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, customer);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, Customer customer)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            customer.RefreshToken = newRefreshToken.Token;
            customer.TokenCreate = newRefreshToken.Created;
            customer.TokenExpier = newRefreshToken.Expires;
        }

        private JwtSecurityToken CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.FirstName),
                new Claim(ClaimTypes.Name, customer.LastName),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role,customer.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return token;
        }

    }
}
