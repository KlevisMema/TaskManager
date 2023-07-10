using AutoMapper;
using TaskManager.DAL.Models;

namespace TaskManager.DTO.Mappers
{
    public class ExeptionLogMapper : Profile
    {
        public ExeptionLogMapper()
        {
            CreateMap<Exception, Logger>();
        }
    }
}