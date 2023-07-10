/*
    This class is responsible for configuring AutoMapper mappings for the Category entity.
    It defines the mappings between CategoryCreateDto, CategoryUpdateDto, and CategoryDto objects to the Category entity.

    AutoMapper is used to automatically map properties from the source object to the destination object.

    - CreateMap<CategoryCreateDto, Category>:
        Maps the properties from CategoryCreateDto to Category entity.
        Sets the CreatedAt property of the Category entity to the current UTC date and time.

    - CreateMap<CategoryUpdateDto, Category>:
        Maps the properties from CategoryUpdateDto to Category entity.
        Sets the EditedAt property of the Category entity to the current UTC date and time.

    - CreateMap<Category, CategoryDto>:
        Maps the properties from Category entity to CategoryDto.
*/

#region Usings
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.DTO_s.Category;
#endregion

namespace TaskManager.DAL.Mappers
{
    /// <summary>
    /// The class responsible for configuring AutoMapper mappings for the Category entity.
    /// </summary>
    public class CategoryMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMappings"/> class.
        /// </summary>
        public CategoryMappings()
        {
            // Maps CategoryCreateDto to Category entity
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Maps CategoryUpdateDto to Category entity
            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.EditedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Maps Category entity to CategoryDto
            CreateMap<Category, CategoryDto>();
        }
    }
}