using FluentValidation.AspNetCore;
using GeoLogBackend.Dominio.Interfaces;
using GeoLogBackend.GeoLogBackend.Api.Configurations;
using GeoLogBackend.Infraestrutura.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using AutoMapper;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Linq;

namespace GeoLogBackend.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }


        public Startup(IHostEnvironment hostEnvironment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                //como só existe dev, manterei assim por enquanto
                //.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddJsonFile($"appsettings.json", true, true)

                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMongoDb();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddControllers();

            //utilizado para adicionar validações do fluent atraves dos
            //assemblies que herdam da classe dele
            services.AddFluentValidation(fv => fv
                .RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.ConfigureSwagger();

            // requer AutoMapper.Extensions.Microsoft.DependencyInjection
            // coisa que o auto mapper não te conta no erro kkkk
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.ResolveDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoLogBackend v1"));
            }

            app.UseRequestLocalization(ops =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new("pt-BR")
                };

                ops.DefaultRequestCulture = new RequestCulture(supportedCultures.First());

                ops.SupportedCultures = supportedCultures;
                ops.SupportedUICultures = supportedCultures;
            });

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }



    }
}
