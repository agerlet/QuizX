using System.Reflection;
using Amazon.Lambda.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedAssembly.Repositories;

namespace SharedAssembly
{
    public static class ServiceProviderExtensions
    {
        public static ServiceProvider GetServiceProvider(this ILambdaContext context)
        {
            var services = new ServiceCollection();
            services
                .AddSingleton<Repository>()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddLogging(_ => _.AddProvider(new CustomLambdaLogProvider(context.Logger)))
                ;
            
            return services.BuildServiceProvider();
        }
    }
}