using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Quiz;
using api.Repositories;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace api.tests.UnitTests
{
    public class babyWhiteCloudCommandHandlerTests
    {
        [Fact]
        public async Task Should_persist_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "123", Answers = new[] { "a", "b", "c", "d", "e" } };
            var babyWhiteCloudCommand = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand);
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(1);
        }
        [Fact]
        public async Task Should_persist_another_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "123", Answers = new[] { "a", "b", "c", "d", "e" } };
            var babyWhiteCloudCommand = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand);
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "def", QuizId = "123", Answers = new[] { "a", "b", "c", "d", "e" } };
            var babyWhiteCloudCommand2 = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand2);
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand, CancellationToken.None);
            await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(2);
        }
        [Fact]
        public async Task Should_update_the_same_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "123", Answers = new[] { "a", "b", "c", "d", "e" } };
            var babyWhiteCloudCommand = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand);
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "abc", QuizId = "123", Answers = new[] { "e", "b", "c", "d", "a" } };
            var babyWhiteCloudCommand2 = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand2);
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand, CancellationToken.None);
            await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(1);
            repository.Query(_ => _.Key == "123").Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_respond_bad_request_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc" };
            var babyWhiteCloudCommand = BabyWhiteCloudCommand.FromQuizAnswerCommand(quizAnswerCommand);
            
            // Act
            var result = await babyWhiteCloudCommandHandler.Handle(babyWhiteCloudCommand, CancellationToken.None);
            
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}