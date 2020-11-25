using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace api.tests.ComponentTests
{
    public static class InMemoryWebServer
    {
        public static async Task<TestServer> CreateServerAsync()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseEnvironment((Environments.Development))
                .UseStartup<Startup>();

            return await Task.FromResult<TestServer>(new TestServer(webHostBuilder));
        }
    }
}