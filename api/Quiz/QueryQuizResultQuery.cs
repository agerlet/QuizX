using System;
using System.Threading;
using System.Threading.Tasks;
using api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Quiz
{
    public class QueryQuizResultQuery : IRequest<IActionResult>
    {
        public string QuizId { get; init; }

        public QueryQuizResultQuery(string quizId)
        {
            QuizId = quizId;
        }
    }

    public class QueryQuizResultQueryHandler : IRequestHandler<QueryQuizResultQuery, IActionResult>
    {
        private readonly Repository _repository;
        private readonly ILogger<QueryQuizResultQueryHandler> _logger;

        public QueryQuizResultQueryHandler(Repository repository, ILogger<QueryQuizResultQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task<IActionResult> Handle(QueryQuizResultQuery request, CancellationToken cancellationToken)
        {
            var results = _repository.Query(_ => request.QuizId == _.Key);
            _logger.LogInformation($"Found {results.Length} answer(s).");
            return Task.FromResult((IActionResult)new OkObjectResult(results));
        }
    }
}