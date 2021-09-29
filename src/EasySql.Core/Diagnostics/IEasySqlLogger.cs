using Microsoft.Extensions.Logging;

namespace EasySql.Diagnostics
{
    public interface IEasySqlLogger
    {
        void Log(EventId eventId, LogLevel logLevel, string message);
    }
}
