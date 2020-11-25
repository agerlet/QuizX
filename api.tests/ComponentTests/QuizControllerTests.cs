using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using api.Quiz;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace api.tests.ComponentTests
{
    public class QuizControllerTests
    {
        [Fact]
        public async Task Should_response_with_200_for_POST()
        {
            // Arrange
            var inMemoryWebServer = await InMemoryWebServer.CreateServerAsync();
            var client = inMemoryWebServer.CreateClient();
            var query = new
            {
                StudentId = "abc",
                Answers = new[] {"a", "b", "c", "d", "e"}
            };
            var request = GetRequest(query);

            // Act
            var response = await client.PostAsync("/api/quiz", request.Content);
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_response_with_200_for_Query()
        {
            // Arrange
            var inMemoryWebServer = await InMemoryWebServer.CreateServerAsync();
            var client = inMemoryWebServer.CreateClient();
            var query = new
            {
                StudentId = "abc",
                Answers = new[] {"a", "b", "c", "d", "e"}
            };
            var request = GetRequest(query);

            // Act
            await client.PostAsync("/api/quiz", request.Content);
            var response = await client.GetAsync("/api/quiz");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            var array = JsonConvert.DeserializeObject<FillBlankCommand[]>(responseContent);
            array.Should().NotBeNullOrEmpty();
            array.Should().HaveCount(1);
        }

        private static HttpRequestMessage GetRequest<T>(T query)
        {
            var payload = JsonConvert.SerializeObject(query);

            var request = new HttpRequestMessage
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            return request;
        }
    }
}