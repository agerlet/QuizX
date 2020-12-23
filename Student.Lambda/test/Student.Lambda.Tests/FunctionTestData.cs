using System.Collections;
using System.Collections.Generic;
using System.Net;
using SharedAssembly.CommandHandlers;
using Student.Lambda;

namespace lambda.Tests
{
    public class PostFunctionTestData : IEnumerable<object[]>
    {
        private List<object[]> _data = new List<object[]>()
        {
            new object[]
            {
                new QuizAnswerCommand
                {
                    QuizId = "Unknown",
                    StudentId = "abc",
                    Answers = new []{ "", "", "", "", "", ""}
                },
                HttpStatusCode.BadRequest,
                "Unknown QuizId should end with BadRequest."
            },
            new object[]
            {
                new QuizAnswerCommand
                {
                    QuizId = "BabyWhiteCloud",
                    StudentId = "abc",
                    Answers = new []{ "", "", "", "", "", ""}
                },
                HttpStatusCode.Created,
                "Handle the known quizId only."
            }
        };
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}