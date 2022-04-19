using Microsoft.Extensions.Logging;

namespace NitecoTest.Context.LogingEvent
{
    public class SingletonLoggerProvider : ILoggerProvider
    {
        ILogger Logger;
        public SingletonLoggerProvider(ILogger logger)
        {
            Logger = logger;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return Logger;
        }
        public void Dispose()
        {
            //NOTHING TO DO
        }
    }
}
