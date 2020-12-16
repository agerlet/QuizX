using api.Models;
using MediatR;

namespace api.Quiz
{
    public class QuizAnswerCommand : INotification
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }

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