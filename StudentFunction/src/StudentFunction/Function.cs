using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedAssembly;
using SharedAssembly.CommandHandlers;
using JsonSerializer = System.Text.Json.JsonSerializer;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace StudentFunction
{

    public class Function
    {
        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var provider = context.GetServiceProvider();
            var mediator = provider.GetService<IMediator>();
            var command = JsonConvert.DeserializeObject<QuizAnswerCommand>(request.Body); 
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
