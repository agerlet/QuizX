using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedAssembly.Models;
using SharedAssembly.Repositories;

namespace SharedAssembly.CommandHandlers
{
    public class QuizResultQuery : IRequest<QuizAnswerModel[]>
    {
        public string QuizId { get; init; }

        public QuizResultQuery(string quizId)
        {
            QuizId = quizId;
        }
    }

    public class QuizResultQueryHandler : IRequestHandler<QuizResultQuery, QuizAnswerModel[]>
    {
        private readonly Repository _repository;
        private readonly ILogger<QuizResultQueryHandler> _logger;

        public QuizResultQueryHandler(Repository repository, ILogger<QuizResultQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task<QuizAnswerModel[]> Handle(QuizResultQuery request, CancellationToken cancellationToken)
        {
            var results = _repository.Query(_ => request.QuizId == _.Key);
            _logger.LogInformation($"Found {results.Length} answer(s).");
            return Task.FromResult(results);
        }
    }
}