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
    public class TwoAncientPoems5CommandHandler : INotificationHandler<QuizAnswerCommand>
    {
        private const string HandleQuizId = "TwoAncientPoems_5";
        private static readonly string[] Answers = { "睡觉", "故乡", "望月", "举手", "楼房", "自己" };

        private readonly Repository _repository;
        private readonly ILogger<TwoAncientPoems5CommandHandler> _logger;

        public TwoAncientPoems5CommandHandler(Repository repository, ILogger<TwoAncientPoems5CommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public Task Handle(QuizAnswerCommand command, CancellationToken cancellationToken)
        {
            if (!command.QuizId.Equals(HandleQuizId, StringComparison.CurrentCultureIgnoreCase)) return Task.CompletedTask;
            _logger.LogInformation($"Received QuizAnswerCommand ({JsonSerializer.Serialize(command)})");
            if (!(command.Answers?.Any() ?? false)) 
            {
                _logger.LogInformation("Missing answers for TwoAncientPoems_5 command.");
                return Task.CompletedTask;
            }
            
            var model = command.ToQuizAnswerModel();
            model.CompleteAt = IsCorrectAnswer(model) ? DateTime.UtcNow : default(DateTime?);
            _repository.Save(model);
            command.Handled();
            return Task.CompletedTask;
        }

        private static bool IsCorrectAnswer(QuizAnswerModel model)
        {
            if (model.Answers.Count(_ => !string.IsNullOrWhiteSpace(_)) != 12) return false;

            var answers = model.Answers
                .Where((c, i) => i % 2 == 0)
                .Zip(model.Answers.Where((c, i) => i % 2 != 0), (first, second) => first + second)
                .OrderBy(_ => _)
                .Join(Answers.OrderBy(_ => _), 
                    _ => _, 
                    _ => _, 
                    (x, y) => x == y);
            
            return answers.Count() == 6;
        }
    }
}