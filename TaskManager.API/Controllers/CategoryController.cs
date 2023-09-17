/*
    This controller is responsible for managing categories in the Task Manager application.
    It provides endpoints for retrieving, creating, updating, and deleting categories.

    The CategoryController inherits from the BaseController, which is a base API controller
    configured with route prefix "api/[controller]". It also includes the necessary dependencies
    for accessing the category service.

    The controller includes the following endpoints:
    - GET /api/Category: Retrieves all categories.
    - GET /api/Category/{id}: Retrieves a category by its ID.
    - POST /api/Category: Creates a new category.
    - PUT /api/Category/{id}: Updates an existing category.
    - PUT /api/Category/RestoreSoftDeletedCategory/{id}: Restores a soft-deleted category.
    - DELETE /api/Category/SoftDelete/{id}: Soft deletes a category.
    - DELETE /api/Category/HardDelete/{id}: Hard deletes a category.

    The methods return an IActionResult containing the corresponding HTTP status code and response.
*/

#region Usings
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.Category;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.API.Controllers
{
    /// <summary>
    /// API controller for managing categories in the Task Manager application.
    /// </summary>
    public class CategoryController : BaseController
    {
        /// <summary>
        /// The category service used for performing category-related operations.
        /// </summary>
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task<ActionResult<Response<List<CategoryDto>>>> 
        GetCategories()
        {
            var response = await _categoryService.GetCategories();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CategoryDto>>>
        GetCategoryById
        (
            Guid id
        )
        {
            var response = await _categoryService.GetCategoryById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The DTO containing the category data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task<ActionResult<Response<CategoryDto>>>
        CreateCategory
        (
            CategoryCreateDto categoryDto
        )
        {
            var response = await _categoryService.CreateCategory(categoryDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The DTO containing the updated category data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<CategoryDto>>> 
        UpdateCategory
        (
            Guid id, CategoryUpdateDto categoryDto
        )
        {
            var response = await _categoryService.UpdateCategory(id, categoryDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Restores a soft deleted category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("RestoreSoftDeletedCategory/{id}")]
        public async Task<ActionResult<Response<CategoryDto>>> 
        RestoreSoftDeletedCategory
        (
            Guid id
        )
        {
            var response = await _categoryService.RestoreSoftDeletedCategory(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Soft deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to soft delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("SoftDelete/{id}")]
        public async Task<ActionResult<Response<bool>>> 
        SoftDeleteCategory
        (
            Guid id
        )
        {
            var response = await _categoryService.SoftDeleteCategory(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Hard deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to hard delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("HardDelete/{id}")]
        public async Task<ActionResult<Response<bool>>> 
        HardDeleteCategory
        (
            Guid id
        )
        {
            var response = await _categoryService.HardDeleteCategory(id);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}