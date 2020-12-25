using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Xunit;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using SharedAssembly.CommandHandlers;
using Student.Lambda;

namespace lambda.Tests
{
    public class PostFunctionTest
    {
        [Theory]
        [ClassData(typeof(PostFunctionTestData))]
        public async Task Function_Tests(QuizAnswerCommand command, int expectedHttpStatusCode, string because)
        {
            // Invoke the Student.Lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest
            {
                Body = JsonSerializer.Serialize(command, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            };
            var response = await Function.Handler(request, context);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(expectedHttpStatusCode, because);
        }
    }
}