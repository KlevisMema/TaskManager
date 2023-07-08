using TaskManager.DAL.Models;
using TaskManager.DAL.Mappers;
using TaskManager.DAL.Context;
using TaskManager.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServicesInterfaces;

namespace TaskManager.API.ProgramEntry
{
    public static class ProgramExtension
    {
        public static IServiceCollection InjectServices
        (
           this IServiceCollection Services,
           IConfiguration Configuration
        )
        {
            Services.AddControllers();
            Services.AddMemoryCache();
            Services.AddEndpointsApiExplorer();

            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();

            Services.AddAutoMapper(typeof(TaskMapper));
            Services.AddAutoMapper(typeof(UserMappers));
            Services.AddAutoMapper(typeof(CommentMappers));
            Services.AddAutoMapper(typeof(ProjectMappings));
            Services.AddAutoMapper(typeof(ExeptionLogMapper));

            Services.AddTransient<ITaskService, TaskService>();
            Services.AddTransient<IUserService, UserService>();
            Services.AddTransient<ILabelService, LabelService>();
            Services.AddTransient<IProjectService, ProjectService>();
            Services.AddTransient<ICommentService, CommentService>();
            Services.AddTransient<IPriorityService, PriorityService>();

            return Services;
        }
    }
}