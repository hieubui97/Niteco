using System.Data;
using Microsoft.Extensions.Logging;

namespace NitecoTest.Context.LogingEvent
{
    public class EntityFrameworkMySQLLogMessage
    {
        public string Elapsed { get; }
        public int CommandTimeout { get; }
        public EventId EventId { get; }
        public string CommandText { get; }
        public string Parameters { get; }
        public CommandType CommandType { get; }
        public EntityFrameworkMySQLLogMessage(EventId eventId, string commandText, string parameters, CommandType commandType, int commandTimeout, string elapsed )
        {
            EventId = eventId;
            CommandText = commandText;
            Parameters = parameters;
            CommandType = commandType;
            Elapsed = elapsed;
            CommandTimeout = commandTimeout;
        }
    }
}
