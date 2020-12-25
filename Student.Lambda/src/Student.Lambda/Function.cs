using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedAssembly;
using SharedAssembly.CommandHandlers;

// This project specifies the serializer used to convert Lambda event into .NET classes in the project's main 
// main function. This assembly register a serializer for use when the project is being debugged using the
// AWS .NET Mock Lambda Test Tool.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace Student.Lambda
{
    public class Function
    {
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>> func = Handler;
            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new DefaultLambdaJsonSerializer());
            using var bootstrap = new LambdaBootstrap(handlerWrapper);
            await bootstrap.RunAsync();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// 
        /// To use this handler to respond to an AWS event, reference the appropriate package from 
        /// https://github.com/aws/aws-Student.Lambda-dotnet#events
        /// and change the string input parameter to the desired event type.
        /// </summary>
        /// <param name="command">A QuizAnswerCommand</param>
        /// <param name="context">ILambdaContext</param>
        /// <returns>System.Threading.Tasks.Task</returns>
        public static async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var provider = context.GetServiceProvider();
            var mediator = provider.GetService<IMediator>();
            var command = JsonSerializer.Deserialize<QuizAnswerCommand>(request.Body,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            await mediator.Publish(command);

            return new APIGatewayProxyResponse
            {
                Body = string.Empty,
                StatusCode = command.IsHandled ? (int) HttpStatusCode.Created : (int) HttpStatusCode.BadRequest,
                Headers = new Dictionary<string, string>
                {
                    {"Access-Control-Allow-Headers", "Content-Type"},
                    {"Access-Control-Allow-Origin", "http://student.quizx.cc"},
                    {"Access-Control-Allow-Methods", "OPTIONS,POST"}
                }
            };
        }
    }
}