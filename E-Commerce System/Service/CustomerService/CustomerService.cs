using AutoMapper;
using E_Commerce_System.Context;
using E_Commerce_System.DTO.CustomerDto;
using E_Commerce_System.Hash;
using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace E_Commerce_System.Service.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _map;

        private readonly IConfiguration _configuration;

        private readonly IHashPassword _hash;

        public CustomerService(ApplicationDBContext context, IMapper map, IConfiguration configuration, IHashPassword hash)
        {
            _context = context;
            _map = map;
            _configuration = configuration;
            _hash = hash;
        }


        public async Task<AuthModel> RegisterUser(RegisterUser user)
        {
            if (await _context.Customers.SingleOrDefaultAsync(c => c.FirstName == user.FirstName) is not null)
                return new AuthModel
                {
                    Message = "First Name Is Already Exists",
                };

            if (await _context.Customers.SingleOrDefaultAsync(c => c.LastName == user.LastName) is not null)
                return new AuthModel
                {
                    Message = "Last Name Is Already Exists"
                };
            if (await _context.Customers.SingleOrDefaultAsync(c => c.Email == user.Email) is not null)
                return new AuthModel
                {
                    Message = "Email  Is Already Exists"
                };
            if (await _context.Customers.SingleOrDefaultAsync(c => c.Phone == user.Phone) is not null)
                return new AuthModel
                {
                    Message = "Phone  Is Already Exists"
                };

            using var imgStream = new MemoryStream();
            await user.Picture.CopyToAsync(imgStream);

            _hash.createHashPassword(user.Password, out byte[] PasswordHash, out byte[] PasswordSlot);


            var customer = _map.Map<Customer>(user);
            customer.Picture = imgStream.ToArray();
            customer.PasswordHash = PasswordHash;
            customer.PasswordSlot = PasswordSlot;




            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();


            return new AuthModel
            {
                Email = user.Email,

                IsAuthenticated = true,
                Username = user.LastName + user.FirstName,
                Role = user.Role,
            };

        }


        public async Task<AuthModel> LoginUser(LoginUser user)
        {
            var users = await _context.Customers.SingleOrDefaultAsync(x => x.LastName == user.LastName && x.FirstName == user.FirstName);






            if (users is null || !_hash.verifyPassword(user.password, users.PasswordHash, users.PasswordSlot))
            {
                return new AuthModel
                {
                    Message = "First Name or Last Name or Password Is Wrong"
                };
            }


            var token = CreateToken(users);

            users.TokenCreate = token.ValidFrom;

            users.TokenExpier = token.ValidTo;

            users.Token = new JwtSecurityTokenHandler().WriteToken(token);

            _context.Customers.Update(users);


            _context.SaveChanges();



            return new AuthModel
            {
                IsAuthenticated = true,
                Email = users.Email,
                ExpiresOn = users.TokenExpier,
                Role = users.Role,
                Username = user.LastName + users.FirstName,
                Token = users.Token,
            };

        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            var customer = await _context.Customers
                .ToListAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            return customer;
        }

        public async Task<Customer> LogOut(int id)
        {
            var customer = await GetCustomerById(id);

            customer.Token = null;

            _context.Customers.Update(customer);

            _context.SaveChanges();

            return customer;
        }

        private JwtSecurityToken CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.FirstName),
                new Claim(ClaimTypes.Name, customer.LastName),
                new Claim(ClaimTypes.Email, customer.Email),
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
