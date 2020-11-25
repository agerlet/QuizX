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
        [Route("api/quiz")]
        public async Task<IActionResult> Get()
        {
            var query = new QueryQuizResultQuery();
            return await _mediator.Send(query);
        }
    }
}