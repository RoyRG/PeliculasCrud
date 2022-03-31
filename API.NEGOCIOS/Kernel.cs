using API.DATA;
using API.NEGOCIOS.Interfaces;
using API.NEGOCIOS.Negocios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.NEGOCIOS
{
    public static class Kernel
    {
        public static IServiceCollection RegistrarNegocios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INegocioCategoria, NegocioCategoria>();
            services.AddTransient<INegocioPelicula, NegocioPelicula>();
            services.AddTransient<INegocioUsuario, NegocioUsuario>();

            services.RegistrarRepositorios<Contexto>(configuration);
            return services;
        }
    }
}
