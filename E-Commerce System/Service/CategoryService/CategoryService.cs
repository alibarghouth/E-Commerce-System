using AutoMapper;
using E_Commerce_System.Context;
using E_Commerce_System.DTO.CategoryDto;
using E_Commerce_System.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_System.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _map;

        public CategoryService(ApplicationDBContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public async Task<Category> addCategoryAsync(RegisterCategory category)
        {
            var categories = _map.Map<Category>(category);


            await _context.Categories
                .AddAsync(categories);

            _context.SaveChanges();

            return categories;
        }



        public async Task<IEnumerable<Category>> getCategoryAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();

            return categories;
        }

        public async Task<Category> returnCategoryById(int id)
        {
            var category = await _context.Categories
                .FindAsync(id);



            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, UpdatedCategory Category)
        {

            var category = await returnCategoryById(id);

            if (!Category.Name.IsNullOrEmpty())
            {
                category.Name = Category.Name;
            }
            if (!Category.Description.IsNullOrEmpty())
            {
                category.Description = Category.Description;
            }

            _context.Categories.Update(category);

            _context.SaveChanges();

            return category;
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var category =await returnCategoryById(id);

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return category;
        }
    }
}
