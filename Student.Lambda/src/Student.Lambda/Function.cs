using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using System;
using System.Net;
using System.Threading.Tasks;
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
            Func<QuizAnswerCommand, ILambdaContext, Task<HttpStatusCode>> func = Handler;
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
        public static async Task<HttpStatusCode> Handler(QuizAnswerCommand command, ILambdaContext context)
        {
            var provider = context.GetServiceProvider();
            var mediator = provider.GetService<IMediator>();
            await mediator.Publish(command);
            
            return command.IsHandled
                ? await Task.FromResult(HttpStatusCode.Created)
                : await Task.FromResult(HttpStatusCode.BadRequest);
        }
    }
}