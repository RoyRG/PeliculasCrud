
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.DATA
{
    public static class Kernel
    {
        public static IServiceCollection RegistrarRepositorios<TContexto>(this IServiceCollection services, IConfiguration configuration) where TContexto : DbContext
        {
            services.AddDbContext<Contexto>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            return services;
        }
    }
}
