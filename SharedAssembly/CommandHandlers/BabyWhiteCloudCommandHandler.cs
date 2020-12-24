using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedAssembly.Repositories;

namespace SharedAssembly.CommandHandlers
{
    public class BabyWhiteCloudCommandHandler : INotificationHandler<QuizAnswerCommand>
    {
        private const string HandleQuizId = "BabyWhiteCloud";
        private static readonly string[] Answers = { "雪花", "变成", "甜", "尝一尝", "甜", "凉凉" };
        
        private readonly Repository _repository;
        private readonly ILogger<BabyWhiteCloudCommandHandler> _logger;

        public BabyWhiteCloudCommandHandler(Repository repository, ILogger<BabyWhiteCloudCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(QuizAnswerCommand command, CancellationToken cancellationToken)
        {
            if (!command.QuizId.Equals(HandleQuizId, StringComparison.CurrentCultureIgnoreCase)) return;
            _logger.LogInformation($"Received QuizAnswerCommand ({JsonSerializer.Serialize(command)})");
            if (!(command.Answers?.Any() ?? false)) 
            {
                _logger.LogInformation("Missing answers for BabyWhiteCloud command.");
                return;
            }

            var model = command.ToQuizAnswerModel();
            model.CompleteAt = model.Answers.SequenceEqual(Answers) ? DateTime.UtcNow : default(DateTime?);
            await _repository.Save(model);
            command.Handled();
        }
    }
}