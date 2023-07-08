using System.Text;
using System.Reflection;
using TaskManager.DAL.Models;
using TaskManager.DAL.Mappers;
using TaskManager.DAL.Context;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.BLL.RepositoryPattern.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManagment.SECURITY.UserAuthenticationService;
using TaskManagment.SECURITY.UserAccountService.Settings;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
using TaskManagment.SECURITY.ApplicationAuthorizationService.ServiceInterface;
using TaskManagment.SECURITY.ApplicationAuthorizationService.ServiceImplementation;

namespace TaskManager.API.ProgramEntry
{
    /// <summary>
    ///     Register all services
    /// </summary>
    public static class ProgramExtension
    {
        /// <summary>
        ///     A method that adds all the services
        /// </summary>
        /// <param name="Services"> The Service Collection </param>
        /// <param name="Configuration"> A Configuration service </param>
        /// <returns> Added services </returns>
        public static IServiceCollection InjectServices
        (
           this IServiceCollection Services,
           IConfiguration Configuration
        )
        {
            Services.AddControllers();
            Services.AddMemoryCache();
            Services.AddEndpointsApiExplorer();

            Services = AddDatabase(Services, Configuration);

            Services = AddAutomapper(Services);

            Services = AddSwagger(Services, Configuration);

            Services = AddServices(Services);

            Services = AddCors(Services, Configuration);

            return Services;
        }

        private static IServiceCollection
        AddAutomapper
        (
            IServiceCollection Services
        )
        {
            Services.AddAutoMapper(typeof(TaskMappings));
            Services.AddAutoMapper(typeof(UserMappings));
            Services.AddAutoMapper(typeof(CommentMappings));
            Services.AddAutoMapper(typeof(ProjectMappings));
            Services.AddAutoMapper(typeof(CategoryMappings));
            Services.AddAutoMapper(typeof(ExeptionLogMapper));

            return Services;
        }

        private static IServiceCollection
        AddServices
        (
            IServiceCollection Services
        )
        {
            Services.AddTransient<ITaskService, TaskService>();
            Services.AddTransient<IUserService, UserService>();
            Services.AddTransient<ILabelService, LabelService>();
            Services.AddTransient<IProjectService, ProjectService>();
            Services.AddTransient<ICommentService, CommentService>();
            Services.AddTransient<ICategoryService, CategoryService>();
            Services.AddTransient<IPriorityService, PriorityService>();

            return Services;
        }

        private static IServiceCollection
        AddSwagger
        (
            IServiceCollection Services,
            IConfiguration Configuration
        )
        {
            // Services Settings
            Services.Configure<AuthenticationSettings>(Configuration.GetSection(AuthenticationSettings.SectionName));
            Services.Configure<ProfilePictureImagePath>(Configuration.GetSection(ProfilePictureImagePath.SectionName));
            var jwtSetting = Configuration.GetSection(AuthenticationSettings.SectionName);

            // Add the custom auth filter, a filter that is used by all enpoints
            Services.AddScoped<IApiKeyAuthorizationFilter>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                string apikey = config!.GetValue<string>("API_KEY")!;
                return new ApiKeyAuthorizationFilter(apikey);
            });
            // Cofigure Authetication
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = Configuration.GetValue<bool>("Jwt:ValidateAudience"),
                       ValidateIssuer = Configuration.GetValue<bool>("Jwt:ValidateIssuer"),
                       ValidateLifetime = Configuration.GetValue<bool>("Jwt:ValidateLifetime"),
                       ValidateIssuerSigningKey = Configuration.GetValue<bool>("Jwt:ValidateIssuerSigningKey"),
                       ValidIssuer = jwtSetting.GetSection("Issuer").Value,
                       ValidAudience = jwtSetting.GetSection("Audience").Value,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.GetSection("Key").Value!)),
                   };
               });

            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Definition").Value, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Name").Value,
                    Type = SecuritySchemeType.ApiKey,
                    Description = Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Description").Value
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = Configuration.GetSection("Swagger:ApplicationAuth:SecurityRequirement:Id").Value },
                            Name = Configuration.GetSection("Swagger:ApplicationAuth:SecurityRequirement:Name").Value,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                options.AddSecurityDefinition(Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Definition").Value, new OpenApiSecurityScheme
                {
                    Description = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Description").Value,
                    Name = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Name").Value,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Scheme").Value,
                    BearerFormat = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:BearerFormat").Value
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Reference:Id").Value
                            },
                            Scheme = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Scheme").Value,
                            Name = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Name").Value,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });


                options.SwaggerDoc(Configuration.GetSection("Swagger:Doc:Version").Value, new OpenApiInfo
                {
                    Version = Configuration.GetSection("Swagger:Doc:Version").Value,
                    Title = Configuration.GetSection("Swagger:Doc:Tittle").Value,
                    License = new OpenApiLicense
                    {
                        Name = Configuration.GetSection("Swagger:Doc:Licence:Name").Value,
                        Url = new Uri(Configuration.GetSection("Swagger:Doc:Licence:Url-Linkedin").Value!)
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            return Services;
        }

        private static IServiceCollection
        AddDatabase
        (
            IServiceCollection Services,
            IConfiguration Configuration
        )
        {
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();

            return Services;
        }

        private static IServiceCollection
        AddCors
        (
            IServiceCollection Services,
            IConfiguration Configuration
        )
        {
            Services.AddCors(options =>
            {
                options.AddPolicy(Configuration.GetSection("Cors:Policy:Name").Value!, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            return Services;
        }
    }
}