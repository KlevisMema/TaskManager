/*
This service class is responsible for managing categories in the Task Manager application.
It uses AutoMapper for object mapping and interacts with the data access layer (DAL) through the ApplicationDbContext.
The CategoryService implements the ICategoryService interface.

Methods:
- GetCategories(): Retrieves all categories from the database.
- GetCategoryById(Guid categoryId): Retrieves a specific category by its ID.
- CreateCategory(CategoryCreateDto categoryDto): Creates a new category.
- UpdateCategory(Guid categoryId, CategoryUpdateDto categoryDto): Updates an existing category.
- SoftDeleteCategory(Guid categoryId): Soft deletes a category by setting the IsDeleted flag to true.
- HardDeleteCategory(Guid categoryId): Hard deletes a category by removing it from the database.
- RestoreSoftDeletedCategory(Guid categoryId): Restores a soft-deleted category by setting the IsDeleted flag to false.

Note: All methods return a Task<Response<T>> object, where T represents the return type of the method.
*/


#region Usings
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.DTO.DTO_s.Category;
using TaskManager.HELPERS.LogsHelper;
using TaskManager.HELPERS.ServiceResponse;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
#endregion

namespace TaskManager.BLL.RepositoryPattern.Services
{
    /// <summary>
    /// This service class is responsible for managing categories in the Task Manager application.
    /// It uses AutoMapper for object mapping and interacts with the data access layer (DAL) through the ApplicationDbContext.
    /// The CategoryService implements the ICategoryService interface.
    /// </summary>
    public class CategoryService : BaseService, ICategoryService
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CategoryService class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="dbContext">The instance of the ApplicationDbContext.</param>
        public CategoryService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a list of <see cref="CategoryDto"/> objects.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving categories.</exception>
        public async Task<Response<List<CategoryDto>>>
        GetCategories()
        {
            try
            {
                var categories = await _dbContext.Categories.Where(x => !x.IsDeleted).ToListAsync();

                return Response<List<CategoryDto>>.Ok(_mapper.Map<List<CategoryDto>>(categories), "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<List<CategoryDto>>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the category.</exception>
        public async Task<Response<CategoryDto>>
        GetCategoryById
        (
            Guid categoryId
        )
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(categoryId);
                if (category == null)
                    return Response<CategoryDto>.NotFound($"Category with id: {categoryId} doesn't exist.");

                return Response<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category), $"Category {category.Name} retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The <see cref="CategoryCreateDto"/> object containing the data for the new category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the category.</exception>
        public async Task<Response<CategoryDto>>
        CreateCategory
        (
            CategoryCreateDto categoryDto
        )
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);

                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();

                await LoggerHelper.LogAction("Careate Category", _dbContext);

                return Response<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category), "Category created successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <param name="categoryDto">The <see cref="CategoryUpdateDto"/> object containing the updated data for the category.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the category.</exception>
        public async Task<Response<CategoryDto>>
        UpdateCategory
        (
            Guid categoryId,
            CategoryUpdateDto categoryDto
        )
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(categoryId);
                if (category == null)
                    return Response<CategoryDto>.NotFound($"Category with id: {categoryId} doesn't exist.");

                _mapper.Map(categoryDto, category);
                await _dbContext.SaveChangesAsync();

                return Response<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category), $"Category with id: {categoryId} updated successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Soft deletes a category by setting the IsDeleted flag to true.
        /// </summary>
        /// <param name="categoryId">The ID of the category to soft delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a boolean indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the category.</exception>
        public async Task<Response<bool>>
        SoftDeleteCategory
        (
            Guid categoryId
        )
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(categoryId);

                if (category == null)
                    return Response<bool>.NotFound($"Category with id: {categoryId} doesn't exist.");

                category.DeletedAt = DateTime.Now;
                category.IsDeleted = true;

                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Category with id: {categoryId} soft deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Hard deletes a category by removing it from the database.
        /// </summary>
        /// <param name="categoryId">The ID of the category to hard delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a boolean indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the category.</exception>
        public async Task<Response<bool>>
        HardDeleteCategory
        (
            Guid categoryId
        )
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(categoryId);

                if (category == null)
                    return Response<bool>.NotFound($"Category with id: {categoryId} doesn't exist.");

                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Category with id: {categoryId} hard deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Restores a soft-deleted category by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="categoryId">The ID of the category to restore.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing a <see cref="CategoryDto"/> object.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the category.</exception>
        public async Task<Response<CategoryDto>>
        RestoreSoftDeletedCategory
        (
            Guid categoryId
        )
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(categoryId);

                if (category == null)
                    return Response<CategoryDto>.NotFound($"Category with id: {categoryId} doesn't exist.");

                if (!category.IsDeleted)
                    return Response<CategoryDto>.UnSuccessMessage($"Category with id: {categoryId} is already active");

                category.EditedAt = DateTime.Now;
                category.IsDeleted = false;

                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();

                return Response<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category), $"Category with id: {categoryId} restored successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }
        #endregion
    }
}