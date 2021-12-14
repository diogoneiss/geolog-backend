using GeoLogBackend.GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Infraestrutura;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Api.Configurations
{
    public static class MongoConfiguration
    {
        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
