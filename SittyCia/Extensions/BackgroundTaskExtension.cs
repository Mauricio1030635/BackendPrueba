using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SittyCia.Data;
using SittyCia.Models;
using SittyCia.Service.IService;
using SittyCia.Service;
using Microsoft.OpenApi.Models;


namespace SittyCia.Extensions
{


    public static class BackgroundTaskExtension
    {
        public static void RegisterBackgroundTask(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("Sql"));
            });
            services.Configure<JwtOptions>(configuration.GetSection("SettingsApi:JwtOptions"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddEndpointsApiExplorer();



            services.AddCors(option => {
                option.AddPolicy("newPolitics", app => { app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

        }


        public static void SwaggerExtension(this IServiceCollection services)
        {

            services.AddSwaggerGen(
             C =>
             {
                 //---INFORMACION
                     C.SwaggerDoc("v1", new OpenApiInfo
                     {
                         Title = "Prueba Tecnica",
                         Version = "v1 ",
                         Description = "Creacion de api para prueba tecnica ",
                         Contact = new OpenApiContact
                         {
                             Name = "Mauricio Medina",
                             Email = "mauriciomedinasierra@gmail.com"
                         }
                     });
                 });
        }
    }
}
