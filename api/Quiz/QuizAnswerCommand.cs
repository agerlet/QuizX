using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Quiz
{
    public class QuizAnswerCommand : IRequest<IActionResult>
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }

        public QuizAnswerModel ToFillBlankModel()
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

    public class QuizAnswerCommandHandler : IRequestHandler<QuizAnswerCommand, IActionResult>
    {
        private readonly Repository _repository;
        private readonly ILogger<QuizAnswerCommandHandler> _logger;
        private static string[] _answers = new[] { "雪花","变成","甜","尝一尝","甜","凉凉" };

        public QuizAnswerCommandHandler(Repository repository, ILogger<QuizAnswerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public Task<IActionResult> Handle(QuizAnswerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received QuizAnswerCommand ({JsonSerializer.Serialize(command)})");
            if (!(command.Answers?.Any() ?? false)) return Task.FromResult((IActionResult) new BadRequestObjectResult(command));
            
            var model = command.ToFillBlankModel();
            model.CompleteAt = model.Answers.SequenceEqual(_answers) ? DateTime.UtcNow : default(DateTime?);
            _repository.Save(model);
            return Task.FromResult((IActionResult)new OkObjectResult(command));
        }
    }
}