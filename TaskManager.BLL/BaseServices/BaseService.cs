using AutoMapper;
using TaskManager.DAL.Context;

namespace TaskManager.BLL.BaseServices
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;

        public BaseService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        )
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
    }
}
