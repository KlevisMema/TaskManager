/*
    This class is responsible for configuring AutoMapper mappings for the Label entity.
    It defines the mappings between LabelCreateDto, LabelUpdateDto, and LabelDto objects to the Label entity.

    AutoMapper is used to automatically map properties from the source object to the destination object.

    - CreateMap<LabelCreateDto, Label>:
        Maps the properties from LabelCreateDto to Label entity.

    - CreateMap<LabelUpdateDto, Label>:
        Maps the properties from LabelUpdateDto to Label entity.

    - CreateMap<Label, LabelDto>:
        Maps the properties from Label entity to LabelDto.
*/

#region Usings
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DTO.DTO_s.Label;
#endregion

namespace TaskManager.DTO.Mappers
{
    /// <summary>
    /// The class responsible for configuring AutoMapper mappings for the Label entity.
    /// </summary>
    public class LabelMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelMappings"/> class.
        /// </summary>
        public LabelMappings()
        {
            // Maps LabelCreateDto to Label entity
            CreateMap<LabelCreateDto, Label>();

            // Maps LabelUpdateDto to Label entity
            CreateMap<LabelUpdateDto, Label>();

            // Maps Label entity to LabelDto
            CreateMap<Label, LabelDto>();
        }
    }
}