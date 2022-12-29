using E_Commerce_System.DTO.CustomerDto;
using E_Commerce_System.DTO.Response.Queries;
using E_Commerce_System.Model;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Service.CustomerService
{
    public interface ICustomerService
    {
        Task<AuthModel> RegisterUser(RegisterUser user);


        Task<AuthModel> LoginUser(LoginUser user);

        Task<IEnumerable<Customer>> GetAllCustomer(string userId=null,PaginationFilter filter =null);

        Task<Customer> GetCustomerById(int id);

        Task<Customer> LogOut(int id);

        IEnumerable<OrderForCustomer> GetOrderItemByCoustomerId();

        Task<Customer> GetCustomerByUserName(LoginUser login);
    }
}
