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
                QuizId = "quiz",
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
            var quizId = "BabyCloudWhite";
            var inMemoryWebServer = await InMemoryWebServer.CreateServerAsync();
            var client = inMemoryWebServer.CreateClient();
            var query = new
            {
                StudentId = "abc",
                QuizId = quizId,
                Answers = new[] {"a", "b", "c", "d", "e"}
            };
            var request = GetRequest(query);

            // Act
            await client.PostAsync("/api/quiz", request.Content);
            var response = await client.GetAsync($"/api/quiz/{quizId}");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_response_with_405_for_Query_without_quizId()
        {
            // Arrange
            var quizId = "quizId";
            var inMemoryWebServer = await InMemoryWebServer.CreateServerAsync();
            var client = inMemoryWebServer.CreateClient();
            var query = new
            {
                StudentId = "abc",
                QuizId = quizId,
                Answers = new[] { "a", "b", "c", "d", "e" }
            };
            var request = GetRequest(query);

            // Act
            await client.PostAsync("/api/quiz", request.Content);
            var response = await client.GetAsync($"/api/quiz");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task Should_response_with_405_for_Query_with_non_exists_quizId()
        {
            // Arrange
            var quizId = "quizId";
            var inMemoryWebServer = await InMemoryWebServer.CreateServerAsync();
            var client = inMemoryWebServer.CreateClient();
            var query = new
            {
                StudentId = "abc",
                QuizId = quizId,
                Answers = new[] { "a", "b", "c", "d", "e" }
            };
            var request = GetRequest(query);

            // Act
            await client.PostAsync("/api/quiz", request.Content);
            var response = await client.GetAsync($"/api/quiz/unknown");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadAsStringAsync();
            var array = JsonConvert.DeserializeObject<QuizAnswerCommand[]>(responseContent);
            array.Should().BeNullOrEmpty();
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