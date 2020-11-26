using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        public FillBlankCommandHandler(Repository repository, ILogger<FillBlankCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public Task<IActionResult> Handle(FillBlankCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received FillBlankCommand ({JsonConvert.SerializeObject(command)})");
            if (!(command.Answers?.Any() ?? false)) return Task.FromResult((IActionResult) new BadRequestObjectResult(command));
            
            _repository.Save(command.ToFillBlankModel());
            return Task.FromResult((IActionResult)new OkObjectResult(command));
        }
    }
}