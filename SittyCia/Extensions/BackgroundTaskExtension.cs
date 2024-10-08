﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SittyCia.Data;
using SittyCia.Core.Models;
using SittyCia.Core.Repository;
using SittyCia.Service;


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

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddControllers();


            var secretKey = configuration.GetSection("SettingsApi:JwtOptions").GetValue<string>("Secret");
            var ValidIssuer = configuration.GetSection("SettingsApi:JwtOptions").GetValue<string>("Issuer");
            var Audience = configuration.GetSection("SettingsApi:JwtOptions").GetValue<string>("Audience");

           


                services.AddAuthentication(options =>
                {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = ValidIssuer,
                    ValidAudience = Audience
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
