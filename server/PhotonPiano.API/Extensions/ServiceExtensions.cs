using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.BusinessLogic.Services;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json;
using System.Text;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentClassRepository, StudentClassRepository>();
            services.AddScoped<IStudentLessonRepository, StudentLessonRepository>();
            services.AddScoped<IEntranceTestRepository, EntranceTestRepository>();
            services.AddScoped<IEntranceTestResultRepository, EntranceTestResultRepository>();
            services.AddScoped<IEntranceTestSlotRepository, EntranceTestSlotRepository>();
            services.AddScoped<ICriteriaRepository, CriteriaRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IClassRepostiory, ClassRepository>();
            return services;
        }

        public static IServiceCollection AddGeneralServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddScoped<IStudentLessonService, StudentLessonService>();
            services.AddScoped<ILocationSerivce, LocationSerivce>();
            services.AddScoped<ICriteriaSerivce, CriteriaSerivce>();
            services.AddScoped<IClassService, ClassSerivce>();
            services.AddScoped<IEntranceTestService, EntranceTestSerivce>();
            services.AddScoped<IEntranceTestSlotService, EntranceTestSlotSerivce>();
            services.AddScoped<IEntranceTestResultService, EntranceTestResultSerivce>();
            return services;
        }
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });
            return services;
        }

        public static IServiceCollection AddSwaggerWithConfigurations(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Photon Piano API V1", Version = "V1.0" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description =
                        @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345example'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>(); // Handles the authorization button
                options.SchemaFilter<DateOnlyDocumentFilter>();
            });
            return services;
        }

        public static IServiceCollection AddControllersWithConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            return services;
        }

        public static IServiceCollection AddCorsWithConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
            return services;
        }


    }
}
