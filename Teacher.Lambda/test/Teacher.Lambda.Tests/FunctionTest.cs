using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Xunit;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using SharedAssembly.Models;

namespace Teacher.Lambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task Should_get_a_collection()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var query = new APIGatewayProxyRequest { PathParameters = new Dictionary<string, string> { {"quizid", "BabyWhiteCloud"}}};
            var answers = await Function.Handler(query, context);

            answers.Should().NotBeNull();
            answers.StatusCode.Should().Be((int) HttpStatusCode.OK);
            var quizAnswers = JsonSerializer.Deserialize<List<QuizAnswerModel>>(answers.Body);
            quizAnswers.Should().NotBeNullOrEmpty();
        }
    }
}