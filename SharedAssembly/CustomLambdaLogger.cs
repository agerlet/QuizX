using System;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;

namespace SharedAssembly
{
    public class CustomLambdaLogger : ILogger
    {
        private string _categoryName;
        private ILambdaLogger _lambdaLogger;
 
        public CustomLambdaLogger(string categoryName, ILambdaLogger lambdaLogger)
        {
            _categoryName = categoryName;
            _lambdaLogger = lambdaLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
 
            _lambdaLogger.LogLine($"{logLevel.ToString()} - {_categoryName} - {formatter(state, exception)}");

        }
    }
}