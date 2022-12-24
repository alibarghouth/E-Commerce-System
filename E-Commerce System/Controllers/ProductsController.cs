using E_Commerce_System.DTO.ProductDto;
using E_Commerce_System.Service.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _productService.GetAllProduct();

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] RegisterProduct product)
        {
            var products = await _productService.AddProduct(product);

            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromForm] UpdatedProduct product)
        {
            var products = await _productService.UpdateProduct(id, product);

            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var products =await _productService.DeleteProduct(id);   

            return Ok(products);
        }
    }
}
