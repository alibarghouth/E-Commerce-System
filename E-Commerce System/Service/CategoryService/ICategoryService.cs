using E_Commerce_System.DTO.CategoryDto;
using E_Commerce_System.Model;

namespace E_Commerce_System.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<Category> addCategoryAsync(RegisterCategory Category);

        Task<IEnumerable<Category>> getCategoryAsync();

        Task<Category> returnCategoryById(int id);

        Task<Category> UpdateCategoryAsync(int id, UpdatedCategory Category);

        Task<Category> DeleteCategoryAsync(int id);
    }
}
