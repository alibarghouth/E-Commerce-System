using E_Commerce_System.DTO.ProductDto;
using E_Commerce_System.Model;

namespace E_Commerce_System.Service.ProductService
{
    public interface IProductService
    {
        Task<Product> AddProduct(RegisterProduct product);

        Task<IEnumerable<Product>> GetAllProduct();

        Task<Product> GetProductById(int id);

        Task<Product> UpdateProduct(int id, UpdatedProduct product);

        Task<Product> DeleteProduct(int id);
    }
}
