using System;
using System.Collections.Generic;
using System.Linq;
using api.Quiz;
using MediatR;

namespace api
{
    public class Repository
    {
        private readonly Dictionary<string, FillBlankCommand> _inMemoryDb = new Dictionary<string, FillBlankCommand>();
        
        public void Save(FillBlankCommand command)
        {
            if (_inMemoryDb.ContainsKey(command.StudentId))
            {
                _inMemoryDb[command.StudentId] = command;
            }
            else
            {
                _inMemoryDb.Add(command.StudentId, command);
            }
        }

        public FillBlankCommand[] Query()
        {
            return _inMemoryDb.Select(_ => _.Value).ToArray();
        }
    }
}