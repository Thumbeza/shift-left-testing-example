using Microsoft.AspNetCore.Mvc.Testing;

namespace We.Sell.Bread.API.Integration.Tests.Utilities
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
            }).UseEnvironment("Development");
        }
    }
}
