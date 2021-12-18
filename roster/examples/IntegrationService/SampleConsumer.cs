using MassTransit;
using Roster.Core.Events;

namespace IntegrationService
{
    public class SampleConsumer : IConsumer<AutomaticDischargeToggled>
    {
        readonly ILogger<SampleConsumer> _logger;

        public SampleConsumer(ILogger<SampleConsumer> logger)
        {
            _logger = logger;    
        }

        public Task Consume(ConsumeContext<AutomaticDischargeToggled> context)
        {
            _logger.LogInformation("Consuming event {@event}", context.Message);
            return Task.CompletedTask;
        }
    }
}