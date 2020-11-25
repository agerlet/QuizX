using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Quiz
{
    public class FillBlankCommand : IRequest<IActionResult>
    {
        public string StudentId { get; set; }
        public string[] Answers { get; set; }
    }

    public class FillBlankCommandHandler : IRequestHandler<FillBlankCommand, IActionResult>
    {
        private readonly Repository _repository;

        public FillBlankCommandHandler(Repository repository)
        {
            _repository = repository;
        }
        
        public Task<IActionResult> Handle(FillBlankCommand command, CancellationToken cancellationToken)
        {
            if (!(command.Answers?.Any() ?? false)) return Task.FromResult((IActionResult) new BadRequestObjectResult(command));
            
            _repository.Save(command);
            return Task.FromResult((IActionResult)new OkObjectResult(command));
        }
    }
}