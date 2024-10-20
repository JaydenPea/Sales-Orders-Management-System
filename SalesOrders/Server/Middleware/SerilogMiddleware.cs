using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace SalesOrders.Server.Middleware
{
    public class SerilogMiddleware : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception != null)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Message", logEvent.Exception.Message));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Stack Trace", logEvent.Exception.StackTrace));
            }

            var errorMessage = logEvent.MessageTemplate.Text;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ErrorMessage", errorMessage));
            }
        }
    }
}
