using AutoMapper;
using E_Commerce_System.DTO.CategoryDto;
using E_Commerce_System.DTO.CustomerDto;
using E_Commerce_System.DTO.OrderDto;
using E_Commerce_System.DTO.OrderItemDto;
using E_Commerce_System.DTO.ProductDto;
using E_Commerce_System.DTO.Response.Queries;
using E_Commerce_System.Model;

namespace E_Commerce_System.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUser, Customer>()
                .ForMember(c => c.PasswordHash, option => option.Ignore())
                .ForMember(c => c.PasswordSlot, option => option.Ignore())
                .ForMember(c => c.Picture, option => option.Ignore());
            CreateMap<RegisterCategory, Category>();

            CreateMap<RegisterProduct, Product>()
                .ForMember(src => src.ProductImg, option => option.Ignore());

            CreateMap<RegisterOrderItem, OrderItem>();
            CreateMap<RegisterOrder, Order>();

            CreateMap<PaginationQueries, PaginationFilter>();
        }
    }
}
