using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Quiz;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace api.tests.UnitTests
{
    public class FillBlankCommandHandlerTests
    {
        [Fact]
        public async Task Should_persist_FillBlankCommand()
        {
            // Arrange
            var repository = new Repository();
            var fillBlankCommandHandler = new FillBlankCommandHandler(repository, NullLogger<FillBlankCommandHandler>.Instance);
            var fillBlankCommand = new FillBlankCommand {StudentId = "abc", Answers = new[] {"a", "b", "c", "d", "e"}};
            
            // Act
            await fillBlankCommandHandler.Handle(fillBlankCommand, CancellationToken.None);
            
            // Assert
            repository.Query().Length.Should().Be(1);
        }
        [Fact]
        public async Task Should_persist_another_FillBlankCommand()
        {
            // Arrange
            var repository = new Repository();
            var fillBlankCommandHandler = new FillBlankCommandHandler(repository, NullLogger<FillBlankCommandHandler>.Instance);
            var fillBlankCommand = new FillBlankCommand {StudentId = "abc", Answers = new[] {"a", "b", "c", "d", "e"}};
            var fillBlankCommand2 = new FillBlankCommand {StudentId = "def", Answers = new[] {"a", "b", "c", "d", "e"}};
            
            // Act
            await fillBlankCommandHandler.Handle(fillBlankCommand, CancellationToken.None);
            await fillBlankCommandHandler.Handle(fillBlankCommand2, CancellationToken.None);
            
            // Assert
            repository.Query().Length.Should().Be(2);
        }
        [Fact]
        public async Task Should_update_the_same_FillBlankCommand()
        {
            // Arrange
            var repository = new Repository();
            var fillBlankCommandHandler = new FillBlankCommandHandler(repository, NullLogger<FillBlankCommandHandler>.Instance);
            var fillBlankCommand = new FillBlankCommand {StudentId = "abc", Answers = new[] {"a", "b", "c", "d", "e"}};
            var fillBlankCommand2 = new FillBlankCommand {StudentId = "abc", Answers = new[] {"e", "b", "c", "d", "a"}};
            
            // Act
            await fillBlankCommandHandler.Handle(fillBlankCommand, CancellationToken.None);
            await fillBlankCommandHandler.Handle(fillBlankCommand2, CancellationToken.None);
            
            // Assert
            repository.Query().Length.Should().Be(1);
            repository.Query().Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_respond_bad_request_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository();
            var fillBlankCommandHandler = new FillBlankCommandHandler(repository, NullLogger<FillBlankCommandHandler>.Instance);
            var fillBlankCommand = new FillBlankCommand {StudentId = "abc"};
            
            // Act
            var result = await fillBlankCommandHandler.Handle(fillBlankCommand, CancellationToken.None);
            
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}