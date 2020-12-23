using System.Collections.Concurrent;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;

namespace SharedAssembly
{
    public class CustomLambdaLogProvider : ILoggerProvider
    {
        private readonly ILambdaLogger _logger;

        private ConcurrentDictionary<string, CustomLambdaLogger> _loggers =>
            new ConcurrentDictionary<string, CustomLambdaLogger>();
        
        public CustomLambdaLogProvider(ILambdaLogger logger)
        {
            _logger = logger;
        }
        
        public void Dispose()
        {
            _loggers.Clear();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, a => new CustomLambdaLogger(a, _logger));
        }
    }
}