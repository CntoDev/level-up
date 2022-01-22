using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Roster.Core.Events;

namespace Roster.Core.Consumers;

public class LoggingConsumer : IConsumer<IEvent>
{
    readonly ILogger<LoggingConsumer> _logger;

    public LoggingConsumer(ILogger<LoggingConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<IEvent> context)
    {
        string json = JsonConvert.SerializeObject(context.Message);
        _logger.LogDebug("Consumer {name} logged {payload}", nameof(LoggingConsumer), json);
        return Task.CompletedTask;
    }
}