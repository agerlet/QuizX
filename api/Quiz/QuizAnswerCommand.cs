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
    }

    public class QuizAnswerCommandHandler : IRequestHandler<QuizAnswerCommand, IActionResult>
    {
        private readonly IMediator _mediator;

        public QuizAnswerCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public Task<IActionResult> Handle(QuizAnswerCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(BabyWhiteCloudCommand.FromQuizAnswerCommand(command));
        }
    }
}