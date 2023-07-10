/*
This interface defines the contract for the CategoryService, which is responsible for managing categories in the Task Manager application.
*/

#region Usings
using TaskManager.DAL.DTO_s.Category;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    /// <summary>
    /// Interface for the CategoryService, responsible for managing categories in the Task Manager application.
    /// </summary>
    public interface ICategoryService
    {
        #region Methods
        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a list of <see cref="CategoryDto"/> objects.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving categories.</exception>
        Task<Response<List<CategoryDto>>>
        GetCategories();

        /// <summary>
        /// Soft deletes a category by setting the IsDeleted flag to true.
        /// </summary>
        /// <param name="categoryId">The ID of the category to soft delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a boolean indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the category.</exception>
        Task<Response<bool>>
        SoftDeleteCategory
        (
            Guid categoryId
        );

        /// <summary>
        /// Hard deletes a category by removing it from the database.
        /// </summary>
        /// <param name="categoryId">The ID of the category to hard delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a boolean indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the category.</exception>
        Task<Response<bool>>
        HardDeleteCategory
        (
            Guid categoryId
        );

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the category.</exception>
        Task<Response<CategoryDto>>
        GetCategoryById
        (
            Guid categoryId
        );

        /// <summary>
        /// Restores a soft-deleted category by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="categoryId">The ID of the category to restore.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the category.</exception>
        Task<Response<CategoryDto>>
        RestoreSoftDeletedCategory
        (
            Guid categoryId
        );

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The <see cref="CategoryCreateDto"/> object containing the data for the new category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the category.</exception>
        Task<Response<CategoryDto>>
        CreateCategory
        (
            CategoryCreateDto categoryDto
        );

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <param name="categoryDto">The <see cref="CategoryUpdateDto"/> object containing the updated data for the category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the category.</exception>
        Task<Response<CategoryDto>>
        UpdateCategory
        (
            Guid categoryId,
            CategoryUpdateDto categoryDto
        );
        #endregion
    }
}