/*
BaseService class serves as the base class for other services in the Task Manager application.
It provides common functionality or dependencies that can be shared among different services.
*/

#region Usings
using AutoMapper;
using TaskManager.DAL.Context;
#endregion

namespace TaskManager.BLL.BaseServices
{
    /// <summary>
    /// Base service class for other services in the Task Manager application.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// The IMapper instance for object mapping.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// The instance of the ApplicationDbContext for database operations.
        /// </summary>
        protected readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the BaseService class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="dbContext">The instance of the ApplicationDbContext.</param>
        public BaseService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
    }
}