using AutoMapper;
using E_Commerce_System.Context;
using E_Commerce_System.DTO.OrderItemDto;
using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_System.Service.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _map;

        public OrderItemService(ApplicationDBContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public async Task<OrderItem> AddOrderItem(RegisterOrderItem orderItem)
        {
            var orderitems = _map.Map<OrderItem>(orderItem);

            await _context.OrderItems.AddAsync(orderitems);
            _context.SaveChanges();

            return orderitems;
        }



        public async Task<IEnumerable<OrderItem>> GetAllOrders()
        {
            var orderItems = await _context.OrderItems
                .ToListAsync();

            return orderItems;
        }


        public async Task<OrderItem> DeleteOrderItem(int id)
        {
            var orderItem = await GetOrderItemById(id);

            _context.OrderItems
               .Remove(orderItem);

            _context.SaveChanges();

            return orderItem;
        }



        public async Task<OrderItem> GetOrderItemById(int id)
        {
            var orderItem = await _context.OrderItems
                .FindAsync(id);

            return orderItem;
        }

        public async Task<OrderItem> UpdateOrderItem(int id, UpdatedOrderItem orderItem)
        {
            var orderItems = await GetOrderItemById(id);

            if(orderItem.Count != null)
            {
                orderItems.Count = orderItem.Count??orderItems.Count;
            }
            if(orderItem.Name != null)
            {
                orderItems.Name = orderItem.Name;
            }
            
            if(orderItem.OrderId != null)
            {
                orderItems.OrderId = orderItem.OrderId??orderItems.OrderId;
            }
            if(orderItem.ProductId != null)
            {
                orderItems.ProductId = orderItem.ProductId??orderItems.ProductId;
            }

            _context.OrderItems.Update(orderItems);

            _context.SaveChanges();

            return orderItems;
        }
    }
}
