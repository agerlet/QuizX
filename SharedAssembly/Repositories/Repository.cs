using System;
using System.Collections.Generic;
using System.Linq;
using SharedAssembly.Models;

namespace SharedAssembly.Repositories
{
    public class Repository
    {
        private readonly Dictionary<string, Dictionary<string, QuizAnswerModel>> _inMemoryDb = new();
        
        public void Save(QuizAnswerModel model)
        {
            if (!_inMemoryDb.ContainsKey(model.QuizId))
            {
                _inMemoryDb.Add(model.QuizId, new Dictionary<string, QuizAnswerModel>());
            }

            var quiz = _inMemoryDb[model.QuizId];
            if (quiz.ContainsKey(model.StudentId))
            {
                quiz[model.StudentId].Answers = model.Answers;
                quiz[model.StudentId].CompleteAt = model.CompleteAt;
            }
            else
            {
                model.ArriveAt = DateTime.UtcNow;
                quiz.Add(model.StudentId, model);
            }
        }

        public QuizAnswerModel[] Query(Func<KeyValuePair<string, Dictionary<string, QuizAnswerModel>>, bool> predicate)
        {
            var (_, value) = _inMemoryDb.SingleOrDefault(predicate);

            return value == null ? Array.Empty<QuizAnswerModel>() : value.Select(_ => _.Value).ToArray();
        }
    }
}