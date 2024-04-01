
using FAMS.API.Middlewares;
using Mapster;
using PhotonPiano.API.Extensions;
using PhotonPiano.Helper.Configuration;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PhotonPianoContext>();

            builder.Services.AddControllersWithConfiguration();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Application services
            builder.Services.AddRepositories()
                            .AddGeneralServices();

            //Third party services

            builder.Services.AddAutoMapper(typeof(MapperConfig))
                            .AddAuthenticationService(builder.Configuration);
            builder.Services.AddMapster();

            builder.Services.AddSwaggerWithConfigurations();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Photon Piano API V1");
                });
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
