using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Newtonsoft.Json;
using SharedAssembly.CommandHandlers;
using Xunit;

namespace StudentFunction.Tests
{
  public class FunctionTest
  {
    [Theory]
    [ClassData(typeof(PostFunctionTestData))]
    public async Task Function_Tests(QuizAnswerCommand command, int expectedHttpStatusCode, string because)
    {
        // Arrange
        var context = new TestLambdaContext();
        var request = new APIGatewayProxyRequest
        {
            Body = JsonConvert.SerializeObject(command)
        };
        var function = new Function();
        
        // Act
        var response = await function.Handler(request, context);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(expectedHttpStatusCode, because);
    }

  }
}