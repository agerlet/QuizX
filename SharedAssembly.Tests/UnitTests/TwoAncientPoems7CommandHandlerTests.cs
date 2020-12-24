using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Extensions.Logging.Abstractions;
using SharedAssembly.CommandHandlers;
using SharedAssembly.Repositories;
using Xunit;

namespace SharedAssembly.Tests.UnitTests
{
    public class TwoAncientPoems7CommandHandlerTests
    {
        private IDynamoDBContext _dbContext;

        public TwoAncientPoems7CommandHandlerTests()
        {
            var client = new AmazonDynamoDBClient();
            _dbContext = new DynamoDBContext(client);
        }
        
        [Fact]
        public async Task Should_handle_TwoAncientPoems_7_Command()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems7CommandHandler(repository, NullLogger<TwoAncientPoems7CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_7", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Should().HaveCount(1);
        }
        [Fact]
        public async Task Should_persist_another_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems7CommandHandler(repository, NullLogger<TwoAncientPoems7CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_7", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "def", QuizId = "TwoAncientPoems_7", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            await handler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_7")).Should().HaveCount(2);
        }
        [Fact]
        public async Task Should_update_the_same_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems7CommandHandler(repository, NullLogger<TwoAncientPoems7CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_7", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_7", Answers = new List<string> { "e", "b", "c", "d", "a" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            await handler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Should().HaveCount(1);
            (await repository.Query("TwoAncientPoems_7", "abc")).Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_not_throw_exception_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems7CommandHandler>.Instance;
            var handler = new TwoAncientPoems7CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_7" };
            Exception exception = null;

            // Act
            try
            {
                await handler.Handle(quizAnswerCommand, CancellationToken.None);
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
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems7CommandHandler>.Instance;
            var handler = new TwoAncientPoems7CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_7",
                Answers = new List<string> {"B", "A", "B", "C", "C"}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Single().CompleteAt.Should().NotBeNull();

            // Arrange
            quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_7",
                Answers = new List<string> {"B", "A", "B", "C", ""}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Single().CompleteAt.Should().BeNull();
        }
        [Fact]
        public async Task Should_not_complete_test_for_incorrect_number_of_answers()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems7CommandHandler>.Instance;
            var handler = new TwoAncientPoems7CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_7",
                Answers = new List<string> {"B", "A", "B", "C", "A"}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Single().CompleteAt.Should().BeNull();
        }
        [Fact]
        public async Task Should_not_complete_test_for_even_number_of_answers()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems7CommandHandler>.Instance;
            var handler = new TwoAncientPoems7CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_7",
                Answers = new List<string> {"B", "A", "B", "", ""}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_7", "abc")).Single().CompleteAt.Should().BeNull();
        }
    }
}