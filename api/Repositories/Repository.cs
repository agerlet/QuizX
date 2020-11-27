using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;

namespace api.Repositories
{
    public class Repository
    {
        private readonly Dictionary<string, FillBlankModel> _inMemoryDb = new Dictionary<string, FillBlankModel>();
        
        public void Save(FillBlankModel model)
        {
            if (_inMemoryDb.ContainsKey(model.StudentId))
            {
                _inMemoryDb[model.StudentId].Answers = model.Answers;
                _inMemoryDb[model.StudentId].CompleteAt = model.CompleteAt;
            }
            else
            {
                model.ArriveAt = DateTime.UtcNow;
                _inMemoryDb.Add(model.StudentId, model);
            }
        }

        public FillBlankModel[] Query()
        {
            return _inMemoryDb.Select(_ => _.Value).ToArray();
        }
    }
}