using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.DAL.DTO_s.Category;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.ServicesInterfaces;

namespace TaskManager.BLL.Services
{
    public class CategoryService : BaseService, ICategoryService 
    {
        public CategoryService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ): base( mapper, dbContext )
        {
            
        }

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

                return Response<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category), "Category created successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<List<CategoryDto>>> 
        GetCategories()
        {
            try
            {
                var categories = await _dbContext.Categories.ToListAsync();

                return Response<List<CategoryDto>>.Ok(_mapper.Map<List<CategoryDto>>(categories), "Categories retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<CategoryDto>>.ErrorMsg(ex.ToString());
            }
        }

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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CategoryDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>> 
        DeleteCategory
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

                return Response<bool>.Ok(true, $"Category with id: {categoryId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }
    }
}