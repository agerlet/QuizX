using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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
            var services = new ServiceCollection()
                    .AddMediatR(Assembly.GetExecutingAssembly())
                    .AddLogging(_ => _.AddProvider(new CustomLambdaLogProvider(context.Logger)))
                    .AddScoped<Repository>()
                    .AddScoped<IDynamoDBContext>(_ =>
                    {
                        var client = new AmazonDynamoDBClient();
                        return new DynamoDBContext(client);
                    })
                    ;
            
            return services.BuildServiceProvider();
        }
    }
}