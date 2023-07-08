using TaskManager.BLL.ServiceResponse;
using TaskManager.DAL.DTO_s.Category;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface ICategoryService
    {
        Task<Response<CategoryDto>> CreateCategory(CategoryCreateDto categoryDto);
        Task<Response<List<CategoryDto>>> GetCategories();
        Task<Response<CategoryDto>> GetCategoryById(Guid categoryId);
        Task<Response<CategoryDto>> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryDto);
        Task<Response<bool>> DeleteCategory(Guid categoryId);
    }
}