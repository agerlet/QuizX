using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace api.Quiz
{
    public class BabyWhiteCloudCommand : INotification
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }

        private BabyWhiteCloudCommand() { }

        public static BabyWhiteCloudCommand FromQuizAnswerCommand(QuizAnswerCommand quizAnswerCommand)
        {
            var command = new BabyWhiteCloudCommand();
            command.StudentId = quizAnswerCommand.StudentId;
            command.QuizId = quizAnswerCommand.QuizId;
            command.Answers = quizAnswerCommand.Answers;
            return command;
        }

        public QuizAnswerModel ToQuizAnswerModel()
        {
            var quizAnswerModel = new QuizAnswerModel
            {
                StudentId = StudentId,
                QuizId = QuizId,
                Answers = Answers
            };
            return quizAnswerModel;
        }
    }

    public class BabyWhiteCloudCommandHandler : INotificationHandler<QuizAnswerCommand>
    {
        private readonly Repository _repository;
        private readonly ILogger<BabyWhiteCloudCommandHandler> _logger;
        private static string[] _answers = new[] { "雪花", "变成", "甜", "尝一尝", "甜", "凉凉" };

        public BabyWhiteCloudCommandHandler(Repository repository, ILogger<BabyWhiteCloudCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Handle(QuizAnswerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received QuizAnswerCommand ({JsonSerializer.Serialize(command)})");
            if (!command.QuizId.Equals("BabyWhiteCloud", StringComparison.CurrentCultureIgnoreCase)) return Task.CompletedTask;
            if (!(command.Answers?.Any() ?? false)) 
            {
                _logger.LogInformation("Missing answers for BabyWhiteCloud command.");
                return Task.CompletedTask;
            }

            var babyWhiteCloudCommand = BabyWhiteCloudCommand.FromQuizAnswerCommand(command);
            var model = babyWhiteCloudCommand.ToQuizAnswerModel();
            model.CompleteAt = model.Answers.SequenceEqual(_answers) ? DateTime.UtcNow : default(DateTime?);
            _repository.Save(model);
            return Task.CompletedTask;
        }
    }
}