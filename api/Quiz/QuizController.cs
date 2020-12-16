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
        public async Task<IActionResult> Post(QuizAnswerCommand command)
        {
            await _mediator.Publish(command);
            return await Task.FromResult(Ok());
        }

        [HttpGet]
        [Route("api/quiz/{quizId}")]
        public async Task<IActionResult> Get(string quizId)
        {
            var query = new QuizResultQuery(quizId);
            return await _mediator.Send(query);
        }
    }
}