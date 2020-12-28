using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharedAssembly;
using SharedAssembly.CommandHandlers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace TeacherFunction
{
    public class Function
    {
        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var provider = context.GetServiceProvider();
            var mediator = provider.GetService<IMediator>();
            var query = new QuizResultQuery(request.PathParameters["quizid"]);
            var quizAnswers = await mediator.Send(query);
            return new APIGatewayProxyResponse
            {
                StatusCode = (int) HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(quizAnswers),
                //Body = JsonSerializer.Serialize(quizAnswers, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase}),
                Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"},
                    {"Access-Control-Allow-Headers", "Content-Type"},
                    {"Access-Control-Allow-Origin", "http://teacher.quizx.cc"},
                    {"Access-Control-Allow-Methods", "OPTIONS,GET"}
                }
            };
        }
    }
}
