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
    public class TwoAncientPoems6CommandHandler : INotificationHandler<QuizAnswerCommand>
    {
        private const string HandleQuizId = "TwoAncientPoems_6";
        private static readonly string[] Answers = { "x", "x", "v", "v" };

        private readonly Repository _repository;
        private readonly ILogger<TwoAncientPoems6CommandHandler> _logger;

        public TwoAncientPoems6CommandHandler(Repository repository, ILogger<TwoAncientPoems6CommandHandler> logger)
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
                _logger.LogInformation("Missing answers for TwoAncientPoems_6 command.");
                return;
            }
            
            var model = command.ToQuizAnswerModel();
            if (model.Answers.All(string.IsNullOrWhiteSpace))
            {
                model.ArriveAt = DateTime.UtcNow;
            }
            model.CompleteAt = IsCorrectAnswer(model) ? DateTime.UtcNow : default(DateTime?);
            await _repository.Save(model);
            command.Handled();
        }

        private static bool IsCorrectAnswer(QuizAnswerModel model)
        {
            return model.Answers.SequenceEqual(Answers);
        }
    }
}