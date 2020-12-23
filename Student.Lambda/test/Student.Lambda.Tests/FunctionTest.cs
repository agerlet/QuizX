using System.Net;
using System.Threading.Tasks;
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
        public async Task Function_Tests(QuizAnswerCommand command, HttpStatusCode expectedHttpStatusCode, string because)
        {
            // Invoke the Student.Lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var httpStatusCode = await Function.Handler(command, context);
            httpStatusCode.Should().Be(expectedHttpStatusCode, because);
        }
    }
}