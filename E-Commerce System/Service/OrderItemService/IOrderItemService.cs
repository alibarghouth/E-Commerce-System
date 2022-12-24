using E_Commerce_System.DTO.OrderItemDto;
using E_Commerce_System.Model;

namespace E_Commerce_System.Service.OrderItemService
{
    public interface IOrderItemService
    {
        Task<OrderItem> AddOrderItem(RegisterOrderItem orderItem);

        Task<IEnumerable<OrderItem>> GetAllOrders();

        Task<OrderItem> DeleteOrderItem(int id);

        Task<OrderItem> GetOrderItemById(int id);

        Task<OrderItem> UpdateOrderItem(int id , UpdatedOrderItem orderItem);
    }
}
