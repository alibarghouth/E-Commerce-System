using AutoMapper;
using E_Commerce_System.Context;
using E_Commerce_System.DTO.ProductDto;
using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_System.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext _context;

        private readonly IMapper _map;

        public ProductService(ApplicationDBContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public async Task<Product> AddProduct(RegisterProduct product)
        {
            using var imgStream = new MemoryStream();
            await product.ProductImg.CopyToAsync(imgStream);

            var map = _map.Map<Product>(product);
            map.ProductImg = imgStream.ToArray();

            await _context.Products.AddAsync(map);
            _context.SaveChanges();

            return map;
        }


        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var product = await _context.Products
                .ToListAsync();

            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public async Task<Product> UpdateProduct(int id, UpdatedProduct product)
        {
            var products = await GetProductById(id);
            if (product.ProductImg != null)
            {
                using var imgStream = new MemoryStream();

                product.ProductImg.CopyToAsync(imgStream);

                products.ProductImg = imgStream.ToArray();

            }
            if (product.Name is not null)

            {
                products.Name = product.Name;

            }

            if (product.Description is not null)
            {

                products.Description = product.Description;

            }
            if (product.CategoryId is not null)
            {
                products.CategoryId = product.CategoryId ?? products.CategoryId;
            } 
            if (product.Price is not null)
            {
                products.Price = product.Price ?? products.Price;
            }

            _context.Products.Update(products);

            _context.SaveChanges();

            return products;


        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await GetProductById(id);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }

    }
}
