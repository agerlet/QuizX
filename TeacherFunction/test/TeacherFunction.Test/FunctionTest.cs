using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using Newtonsoft.Json;
using SharedAssembly.Models;
using Xunit;

namespace TeacherFunction.Tests
{
  public class FunctionTest
  {
    [Fact]
    public async Task Should_get_a_collection()
    {
        // Arrange
        var context = new TestLambdaContext();
        var query = new APIGatewayProxyRequest { PathParameters = new Dictionary<string, string> { {"quizid", "BabyWhiteCloud"}}};
        var function = new Function();
        
        // Act
        var answers = await function.Handler(query, context);

        // Assert
        answers.Should().NotBeNull();
        answers.StatusCode.Should().Be((int) HttpStatusCode.OK);
        var quizAnswers = JsonConvert.DeserializeObject<List<QuizAnswerModel>>(answers.Body);
        quizAnswers.Should().NotBeNull();
    }
  }
}