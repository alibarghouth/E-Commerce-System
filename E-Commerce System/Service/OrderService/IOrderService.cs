using E_Commerce_System.DTO.OrderDto;
using E_Commerce_System.DTO.OrderItemDto;
using E_Commerce_System.Model;

namespace E_Commerce_System.Service.OrderService
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(RegisterOrder order);

        Task<Order> GetOrderById(int id);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order> DeleteOrder(int id);

        Task<Order> UpdateOrder(int id, UpdateOrder order);
    }
}
