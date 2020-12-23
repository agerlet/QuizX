using MediatR;
using SharedAssembly.Models;

namespace SharedAssembly.CommandHandlers
{
    public class QuizAnswerCommand : INotification
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }
        public bool IsHandled { get; private set; }

        public void Handled() => IsHandled = true;

        public QuizAnswerModel ToQuizAnswerModel()
        {
            var quizAnswerModel = new QuizAnswerModel
            {
                StudentId = StudentId,
                QuizId = QuizId,
                Answers = Answers
            };
            return quizAnswerModel;
        }
    }
}