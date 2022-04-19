using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace NitecoTest.Context.LogingEvent
{
    public class EntityFrameworkMySqlLogger : ILogger
    {
        Action<EntityFrameworkMySQLLogMessage> LogMessage;
        public EntityFrameworkMySqlLogger(Action<EntityFrameworkMySQLLogMessage> logMessage)
        {
            LogMessage = logMessage;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return default;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Id != 20101)
            {
                //Filter messages that aren't relevant.
                //There may be other types of messages that are relevant for other database platforms...
                return;
            }
            if (state is IReadOnlyList<KeyValuePair<string, object>> keyValuePairList)
            {
                var entityFrameworkSqlLogMessage = new EntityFrameworkMySQLLogMessage
                (
                    eventId,
                    (string)keyValuePairList.FirstOrDefault(k => k.Key == "commandText").Value,
                    (string)keyValuePairList.FirstOrDefault(k => k.Key == "parameters").Value,
                    (CommandType)keyValuePairList.FirstOrDefault(k => k.Key == "commandType").Value,
                    (int)keyValuePairList.FirstOrDefault(k => k.Key == "commandTimeout").Value,
                    (string)keyValuePairList.FirstOrDefault(k => k.Key == "elapsed").Value
                );
                LogMessage(entityFrameworkSqlLogMessage);
            }
        }
    }
}
