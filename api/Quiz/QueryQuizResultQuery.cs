using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Quiz
{
    public class QueryQuizResultQuery : IRequest<IActionResult>
    {
    }

    public class QueryQuizResultQueryHandler : IRequestHandler<QueryQuizResultQuery, IActionResult>
    {
        private readonly Repository _repository;

        public QueryQuizResultQueryHandler(Repository repository)
        {
            _repository = repository;
        }
        public Task<IActionResult> Handle(QueryQuizResultQuery request, CancellationToken cancellationToken)
        {
            var results = _repository.Query();
            return Task.FromResult((IActionResult)new OkObjectResult(results)); 
        }
    }
}