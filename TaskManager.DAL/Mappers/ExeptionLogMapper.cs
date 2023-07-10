using AutoMapper;
using TaskManager.DAL.Models;

namespace TaskManager.DAL.Mappers
{
    public class ExeptionLogMapper : Profile
    {
        public ExeptionLogMapper()
        {
            CreateMap<Exception, Logger>();
        }
    }
}