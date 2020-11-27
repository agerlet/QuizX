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
    public class FillBlankCommand : IRequest<IActionResult>
    {
        public string StudentId { get; set; }
        public string[] Answers { get; set; }

        public FillBlankModel ToFillBlankModel()
        {
            var fillBlankModel = new FillBlankModel
            {
                StudentId = StudentId,
                Answers = Answers
            };
            return fillBlankModel;
        }
    }

    public class FillBlankCommandHandler : IRequestHandler<FillBlankCommand, IActionResult>
    {
        private readonly Repository _repository;
        private readonly ILogger<FillBlankCommandHandler> _logger;
        private static string[] _answers = new[] { "雪花","变成","甜","尝一尝","甜","凉凉" };

        public FillBlankCommandHandler(Repository repository, ILogger<FillBlankCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public Task<IActionResult> Handle(FillBlankCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received FillBlankCommand ({JsonSerializer.Serialize(command)})");
            if (!(command.Answers?.Any() ?? false)) return Task.FromResult((IActionResult) new BadRequestObjectResult(command));
            
            var model = command.ToFillBlankModel();
            model.CompleteAt = model.Answers.SequenceEqual(_answers) ? DateTime.UtcNow : default(DateTime?);
            _repository.Save(model);
            return Task.FromResult((IActionResult)new OkObjectResult(command));
        }
    }
}