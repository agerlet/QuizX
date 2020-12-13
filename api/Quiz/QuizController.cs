using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Quiz
{
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/quiz")]
        public async Task<IActionResult> Post(FillBlankCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        [Route("api/quiz/{quizId}")]
        public async Task<IActionResult> Get(string quizId)
        {
            var query = new QueryQuizResultQuery(quizId);
            return await _mediator.Send(query);
        }
    }
}