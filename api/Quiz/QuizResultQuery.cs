using System;
using System.Threading;
using System.Threading.Tasks;
using api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Quiz
{
    public class QuizResultQuery : IRequest<IActionResult>
    {
        public string QuizId { get; init; }

        public QuizResultQuery(string quizId)
        {
            QuizId = quizId;
        }
    }

    public class QuizResultQueryHandler : IRequestHandler<QuizResultQuery, IActionResult>
    {
        private readonly Repository _repository;
        private readonly ILogger<QuizResultQueryHandler> _logger;

        public QuizResultQueryHandler(Repository repository, ILogger<QuizResultQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task<IActionResult> Handle(QuizResultQuery request, CancellationToken cancellationToken)
        {
            var results = _repository.Query(_ => request.QuizId == _.Key);
            _logger.LogInformation($"Found {results.Length} answer(s).");
            return Task.FromResult((IActionResult)new OkObjectResult(results));
        }
    }
}