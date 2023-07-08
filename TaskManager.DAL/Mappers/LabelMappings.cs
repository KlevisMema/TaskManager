using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.DTO_s.Label;

namespace TaskManager.DAL.Mappers
{
    public class LabelMappings : Profile
    {
        public LabelMappings() 
        {
            CreateMap<LabelCreateDto, Label>();
            CreateMap<LabelUpdateDto, Label>();
            CreateMap<Label, LabelDto>();
        }
    }
}