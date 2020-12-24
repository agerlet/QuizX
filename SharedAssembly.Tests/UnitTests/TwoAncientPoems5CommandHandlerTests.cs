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
    public class TwoAncientPoems5CommandHandlerTests
    {
        private IDynamoDBContext _dbContext;

        public TwoAncientPoems5CommandHandlerTests()
        {
            var client = new AmazonDynamoDBClient();
            _dbContext = new DynamoDBContext(client);
        }
        [Fact]
        public async Task Should_handle_TwoAncientPoems_5_Command()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems5CommandHandler(repository, NullLogger<TwoAncientPoems5CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_5", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_5")).Should().HaveCount(1);
        }
        [Fact]
        public async Task Should_persist_another_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems5CommandHandler(repository, NullLogger<TwoAncientPoems5CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_5", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "def", QuizId = "TwoAncientPoems_5", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            await handler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_5")).Should().HaveCount(2);
        }
        [Fact]
        public async Task Should_update_the_same_BabyWhiteCloudCommand()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var handler = new TwoAncientPoems5CommandHandler(repository, NullLogger<TwoAncientPoems5CommandHandler>.Instance);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_5", Answers = new List<string> { "a", "b", "c", "d", "e" } };
            var quizAnswerCommand2 = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_5", Answers = new List<string> { "e", "b", "c", "d", "a" } };
            
            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);
            await handler.Handle(quizAnswerCommand2, CancellationToken.None);
            
            // Assert
            (await repository.Query("TwoAncientPoems_5")).Should().HaveCount(1);
            (await repository.Query("TwoAncientPoems_5")).Single().Answers.Should().IsSameOrEqualTo(new[] {"e", "b", "c", "d", "a"});
        }
        [Fact]
        public async Task Should_not_throw_exception_when_answers_are_missing_or_empty()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems5CommandHandler>.Instance;
            var handler = new TwoAncientPoems5CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand { StudentId = "abc", QuizId = "TwoAncientPoems_5" };
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
            var logger = NullLogger<TwoAncientPoems5CommandHandler>.Instance;
            var handler = new TwoAncientPoems5CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_5",
                Answers = new List<string> {"睡", "觉", "故", "乡", "望", "月", "举", "手", "楼", "房", "自", "己"}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_5")).Single().CompleteAt.Should().NotBeNull();

            // Arrange
            quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_5",
                Answers = new List<string> {"睡", "觉", "故", "乡", "望", "月", "举", "手", "楼", "房", "", ""}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_5")).Single().CompleteAt.Should().BeNull();
        }
        [Fact]
        public async Task Should_not_complete_test_for_incorrect_number_of_answers()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems5CommandHandler>.Instance;
            var handler = new TwoAncientPoems5CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_5",
                Answers = new List<string> {"睡", "觉", "故", "乡", "望", "月", "举", "手", "楼", "房", "", ""}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_5")).Single().CompleteAt.Should().BeNull();
        }
        [Fact]
        public async Task Should_not_complete_test_for_odd_number_of_answers()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems5CommandHandler>.Instance;
            var handler = new TwoAncientPoems5CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_5",
                Answers = new List<string> {"睡", "觉", "故", "乡", "望", "月", "举", "手", "楼", "房", "自"}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_5")).Single().CompleteAt.Should().BeNull();
        }
        [Fact]
        public async Task Should_complete_test_when_answer_is_in_random_order()
        {
            // Arrange
            var repository = new Repository(_dbContext);
            var logger = NullLogger<TwoAncientPoems5CommandHandler>.Instance;
            var handler = new TwoAncientPoems5CommandHandler(repository, logger);
            var quizAnswerCommand = new QuizAnswerCommand
            {
                StudentId = "abc", QuizId = "TwoAncientPoems_5",
                Answers = new List<string> {"故", "乡", "睡", "觉", "楼", "房", "望", "月", "自", "己", "举", "手"}
            };

            // Act
            await handler.Handle(quizAnswerCommand, CancellationToken.None);

            // Assert
            (await repository.Query("TwoAncientPoems_5")).Single().CompleteAt.Should().NotBeNull();
        }
    }
}