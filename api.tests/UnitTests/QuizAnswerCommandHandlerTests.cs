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
    public class QuizAnswerCommandHandlerTests
    {
        [Fact]
        public async Task Should_persist_QuizAnswerCommand()
        {
            // Arrange
            var repository = new Repository();
            var QuizAnswerCommandHandler = new QuizAnswerCommandHandler(repository, NullLogger<QuizAnswerCommandHandler>.Instance);
            var QuizAnswerCommand = new QuizAnswerCommand {StudentId = "abc", QuizId = "123", Answers = new[] {"a", "b", "c", "d", "e"}};
            
            // Act
            await QuizAnswerCommandHandler.Handle(QuizAnswerCommand, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(1);
        }
        [Fact]
        public async Task Should_persist_another_QuizAnswerCommand()
        {
            // Arrange
            var repository = new Repository();
            var QuizAnswerCommandHandler = new QuizAnswerCommandHandler(repository, NullLogger<QuizAnswerCommandHandler>.Instance);
            var QuizAnswerCommand = new QuizAnswerCommand {StudentId = "abc", QuizId = "123", Answers = new[] {"a", "b", "c", "d", "e"}};
            var QuizAnswerCommand2 = new QuizAnswerCommand {StudentId = "def", QuizId = "123", Answers = new[] {"a", "b", "c", "d", "e"}};
            
            // Act
            await QuizAnswerCommandHandler.Handle(QuizAnswerCommand, CancellationToken.None);
            await QuizAnswerCommandHandler.Handle(QuizAnswerCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(2);
        }
        [Fact]
        public async Task Should_update_the_same_QuizAnswerCommand()
        {
            // Arrange
            var repository = new Repository();
            var QuizAnswerCommandHandler = new QuizAnswerCommandHandler(repository, NullLogger<QuizAnswerCommandHandler>.Instance);
            var QuizAnswerCommand = new QuizAnswerCommand {StudentId = "abc", QuizId = "123", Answers = new[] {"a", "b", "c", "d", "e"}};
            var QuizAnswerCommand2 = new QuizAnswerCommand {StudentId = "abc", QuizId = "123", Answers = new[] {"e", "b", "c", "d", "a"}};
            
            // Act
            await QuizAnswerCommandHandler.Handle(QuizAnswerCommand, CancellationToken.None);
            await QuizAnswerCommandHandler.Handle(QuizAnswerCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "123").Length.Should().Be(1);
            repository.Query(_ => _.Key == "123").Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_respond_bad_request_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository();
            var QuizAnswerCommandHandler = new QuizAnswerCommandHandler(repository, NullLogger<QuizAnswerCommandHandler>.Instance);
            var QuizAnswerCommand = new QuizAnswerCommand {StudentId = "abc"};
            
            // Act
            var result = await QuizAnswerCommandHandler.Handle(QuizAnswerCommand, CancellationToken.None);
            
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}