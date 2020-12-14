using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Quiz
{
    public class QuizAnswerCommand : INotification
    {
        public string StudentId { get; set; }
        public string QuizId { get; set; }
        public string[] Answers { get; set; }
    }
}