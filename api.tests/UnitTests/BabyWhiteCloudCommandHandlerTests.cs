using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Quiz;
using api.Quiz.CommandHandlers;
using api.Repositories;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace api.tests.UnitTests
{
    public class BabyWhiteCloudCommandHandlerTests
    {
        [Fact]
        public async Task Should_persist_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "a", "b", "c", "d", "e" } };
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "BabyWhiteCloud").Length.Should().Be(1);
        }
        [Fact]
        public async Task Should_persist_another_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "def", QuizId = "BabyWhiteCloud", Answers = new[] { "a", "b", "c", "d", "e" } };
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "BabyWhiteCloud").Length.Should().Be(2);
        }
        [Fact]
        public async Task Should_update_the_same_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository();
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, NullLogger<BabyWhiteCloudCommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "e", "b", "c", "d", "a" } };
            
            // Act
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            repository.Query(_ => _.Key == "BabyWhiteCloud").Should().HaveCount(1);
            repository.Query(_ => _.Key == "BabyWhiteCloud").Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_not_throw_exception_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository();
            var logger = NullLogger<BabyWhiteCloudCommandHandler>.Instance;
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud" };
            Exception exception = null;

            // Act
            try
            {
                await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            // Assert
            exception.Should().BeNull();
        }
        
        [Fact]
        public async Task Should_complete_test_when_answers_are_correct()
        {
            // Arrange
            var repository = new Repository();
            var logger = NullLogger<BabyWhiteCloudCommandHandler>.Instance;
            var babyWhiteCloudCommandHandler = new BabyWhiteCloudCommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "雪花", "变成", "甜", "尝一尝", "甜", "凉凉" } };

            // Act
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            repository.Query(_ => _.Key == "BabyWhiteCloud").Single().CompleteAt.Should().NotBeNull();
            
            // Arrange
            quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "BabyWhiteCloud", Answers = new[] { "雪花", "变成", "甜", "尝一尝", "甜", "" } };

            // Act
            await babyWhiteCloudCommandHandler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            repository.Query(_ => _.Key == "BabyWhiteCloud").Single().CompleteAt.Should().BeNull();
        }
    }
}