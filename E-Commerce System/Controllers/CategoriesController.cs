using E_Commerce_System.DTO.CategoryDto;
using E_Commerce_System.Service.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(RegisterCategory category)
        {
            if (category == null) 
            {
                return BadRequest("Error in Input");
            }
             var categoryies = await _categoryService.addCategoryAsync(category);
            
            return Ok(categoryies);
        }

        [HttpGet("getCategory")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var categories =await _categoryService.getCategoryAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCtategoryById(int id)
        {
            var category = await _categoryService.returnCategoryById(id);
            
            return Ok(category);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.DeleteCategoryAsync(id);


            return Ok(category);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id , UpdatedCategory category)
        {
            var categoryies = await _categoryService.UpdateCategoryAsync(id, category);

            return Ok(categoryies);
        }
    }
}
