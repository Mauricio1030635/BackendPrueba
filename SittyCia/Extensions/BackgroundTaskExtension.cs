using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SittyCia.Data;
using SittyCia.Models;
using SittyCia.Service.IService;
using SittyCia.Service;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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


            var secretKey = configuration.GetSection("SettingsApi:JwtOptions").GetValue<string>("Secret");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
             });


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
                 //---INICIO Interfaz autenticacion
                 
                 C.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                             In = ParameterLocation.Header,
                             Description = "Ingrese Bearer [token]\r\n\r\n"
                                             + "Ejemplo: Bearer Token",

                             Name = "Authorization",
                             Type = SecuritySchemeType.ApiKey
                         });
                         C.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                  new OpenApiSecurityScheme
                  {
                  Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  },
                      Scheme="oauth2",
                      Name="Bearer",
                      In = ParameterLocation.Header,
                  },
                  new List<string> { }
                } });



             });


        }
    }
}
