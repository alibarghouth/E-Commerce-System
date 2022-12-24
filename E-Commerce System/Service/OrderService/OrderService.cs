using AutoMapper;
using E_Commerce_System.Context;
using E_Commerce_System.DTO.OrderDto;
using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_System.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _mapper;


        public OrderService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Order> AddOrderAsync(RegisterOrder order)
        {
            var orders = _mapper.Map<Order>(order);

            await _context.Orders.AddAsync(orders);

            _context.SaveChanges();

            return orders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .ToListAsync();

            return orders;
        }


        public async Task<Order> DeleteOrder(int id)
        {
            var order = await GetOrderById(id);

            _context.Orders.Remove(order);

            _context.SaveChanges();

            return order;
        }

 


        public async Task<Order> UpdateOrder(int id, UpdateOrder order)
        {
            var orders = await GetOrderById(id);    

            if(order.CustomerId is not null)
            {
                orders.CustomerId = order.CustomerId??orders.CustomerId;
            }

            if(order.Name is not null)
            {
                orders.Name = order.Name;
            }

            _context.Orders.Update(orders);

            _context.SaveChanges();

            return orders;
        }
    }
}
