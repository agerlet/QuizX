using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.TestUtilities;
using FluentAssertions;
using SharedAssembly.CommandHandlers;

namespace Teacher.Lambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task Should_get_empty_collection_in_the_beginning()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var query = new QuizResultQuery("BabyWhiteCloud");
            var answers = await Function.Handler(query, context);

            answers.Should().NotBeNull();
            answers.Should().BeEmpty();
        }
    }
}