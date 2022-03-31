using API.NEGOCIOS;
using API.SERVICIOS.AutoMapper;
using API.SERVICIOS.Interfaces;
using API.SERVICIOS.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace API.SERVICIOS
{
    public static class Kernel
    {
       public static IServiceCollection RegistrarServicios(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IServicioCategoria, ServicioCategoria>();
            services.AddTransient<IServicioPelicula, ServicioPelicula>();
            services.AddTransient<IServicioUsuario, ServicioUsuario>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoriaProfile());

            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiPeliculasCategorias", new OpenApiInfo()
                {
                    Title = "API Categorías Películas",
                    Version = "1",
                    Description = "Backend películas",
                    Contact = new OpenApiContact()
                    {
                        Email = "roy.garcia@qacg.com",
                        Name = "Roy García",
                        
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                options.SwaggerDoc("ApiPeliculas", new OpenApiInfo()
                {
                    Title = "API Pel�culas",
                    Version = "1",
                    Description = "Backend películas",
                    Contact = new OpenApiContact()
                    {
                        Email = "roy.garcia@qacg.com",
                        Name = "Roy García",
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                options.SwaggerDoc("ApiPeliculasUsuarios", new OpenApiInfo()
                {
                    Title = "API Usuarios Películas",
                    Version = "1",
                    Description = "Backend películas",
                    Contact = new OpenApiContact()
                    {
                        Email = "roy.garcia@qacg.com",
                        Name = "Roy García",
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                var archivoXmlComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutaApiComentarios = Path.Combine(AppContext.BaseDirectory, archivoXmlComentarios);
                //options.IncludeXmlComments(rutaApiComentarios);

                options.IncludeXmlComments($"C:\\Users\\QUALITY\\Desktop\\API.PELICULA\\API.PELICULA\\ApiPeliculas.xml");

                //Primero definir el esquema de seguridad
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Autenticación JWT (Bearer)",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                   }
                  });


            });

            services.RegistrarNegocios(configuration);
            return services;
        }
    }
}
