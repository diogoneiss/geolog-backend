using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.Infraestrutura.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            //services.AddScoped<IPaisService, IPaisService>();
            services.AddScoped<IIbgeProvider, IbgeProvider>();

            return services;
        }
    }
}
