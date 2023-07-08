using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.DTO_s.Category;

namespace TaskManager.DAL.Mappers
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings() 
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}