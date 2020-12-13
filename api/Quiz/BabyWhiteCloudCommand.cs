using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Quiz
{
    public class BabyWhiteCloudCommand : IRequest<IActionResult>
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

    public class BabyWhiteCloudCommandHandler : IRequestHandler<BabyWhiteCloudCommand, IActionResult>
    {
        private readonly Repository _repository;
        private readonly ILogger<BabyWhiteCloudCommandHandler> _logger;
        private static string[] _answers = new[] { "雪花", "变成", "甜", "尝一尝", "甜", "凉凉" };

        public BabyWhiteCloudCommandHandler(Repository repository, ILogger<BabyWhiteCloudCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public Task<IActionResult> Handle(BabyWhiteCloudCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received BabyWhiteCloudCommand ({JsonSerializer.Serialize(command)})");
            if (!(command.Answers?.Any() ?? false)) return Task.FromResult((IActionResult)new BadRequestObjectResult(command));

            var model = command.ToQuizAnswerModel();
            model.CompleteAt = model.Answers.SequenceEqual(_answers) ? DateTime.UtcNow : default(DateTime?);
            _repository.Save(model);
            return Task.FromResult((IActionResult)new OkObjectResult(command));
        }
    }
}