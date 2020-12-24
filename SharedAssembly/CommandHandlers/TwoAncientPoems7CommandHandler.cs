using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedAssembly.Models;
using SharedAssembly.Repositories;

namespace SharedAssembly.CommandHandlers
{
    public class TwoAncientPoems7CommandHandler : INotificationHandler<QuizAnswerCommand>
    {
        private const string HandleQuizId = "TwoAncientPoems_7";
        private static readonly string[] Answers = { "B", "A", "B", "C", "C" };

        private readonly Repository _repository;
        private readonly ILogger<TwoAncientPoems7CommandHandler> _logger;

        public TwoAncientPoems7CommandHandler(Repository repository, ILogger<TwoAncientPoems7CommandHandler> logger)
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
                _logger.LogInformation("Missing answers for TwoAncientPoems_7 command.");
                return;
            }
            
            var model = command.ToQuizAnswerModel();
            model.CompleteAt = IsCorrectAnswer(model) ? DateTime.UtcNow : default(DateTime?);
            await _repository.Save(model);
            command.Handled();
        }

        private static bool IsCorrectAnswer(QuizAnswerModel model)
        {
            if (model.Answers.Count(_ => !string.IsNullOrWhiteSpace(_)) != 5) return false;

            return model.Answers.SequenceEqual(Answers);
        }
    }
}