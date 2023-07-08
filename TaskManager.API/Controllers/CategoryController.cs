using Microsoft.AspNetCore.Mvc;
using TaskManager.DAL.DTO_s.Category;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _categoryService.GetCategories();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var response = await _categoryService.GetCategoryById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The DTO containing the category data.</param>
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryDto)
        {
            var response = await _categoryService.CreateCategory(categoryDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The DTO containing the updated category data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryDto)
        {
            var response = await _categoryService.UpdateCategory(id, categoryDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var response = await _categoryService.DeleteCategory(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}