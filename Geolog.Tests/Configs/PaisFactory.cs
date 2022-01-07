using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace GeoLogBackend.Api.Tests
{
    public class PaisFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureWebHostDefaults(x =>
                              {
                                  x.UseStartup<TStartup>().UseTestServer();
                              });
            return builder;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Testing");

            //base.CreateHostBuilder();

        }
    }
}
