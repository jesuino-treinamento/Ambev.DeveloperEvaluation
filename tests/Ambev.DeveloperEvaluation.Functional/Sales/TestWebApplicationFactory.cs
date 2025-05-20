using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ambev.DeveloperEvaluation.Functional.Sales
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<ISaleRepository> SaleRepositoryMock { get; } = new Mock<ISaleRepository>();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.FirstOrDefault(
                    d => d.ServiceType == typeof(ISaleRepository));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(SaleRepositoryMock.Object);

                services.AddLogging(config =>
                {
                    config.ClearProviders();
                    config.AddDebug();
                    config.AddConsole();
                });
            });

            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            base.ConfigureWebHost(builder);
        }
    }
}
