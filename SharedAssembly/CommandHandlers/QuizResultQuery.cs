using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedAssembly.Models;
using SharedAssembly.Repositories;

namespace SharedAssembly.CommandHandlers
{
    public class QuizResultQuery : IRequest<List<QuizAnswerModel>>
    {
        public string QuizId { get; }

        public QuizResultQuery(string quizId)
        {
            QuizId = quizId;
        }
    }

    public class QuizResultQueryHandler : IRequestHandler<QuizResultQuery, List<QuizAnswerModel>>
    {
        private readonly Repository _repository;
        private readonly ILogger<QuizResultQueryHandler> _logger;

        public QuizResultQueryHandler(Repository repository, ILogger<QuizResultQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<List<QuizAnswerModel>> Handle(QuizResultQuery request, CancellationToken cancellationToken)
        {
            var results = await _repository.Query(request.QuizId);
            _logger.LogInformation($"Found {results.Count} answer(s).");
            return results;
        }
    }
}