using MediatR;

namespace api.Quiz
{
    public class QuizAnswerCommand : INotification
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }
    }
}