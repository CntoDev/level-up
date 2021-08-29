using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Events;

namespace Roster.Infrastructure.Consumers {
    public class SampleConsumer : IConsumer<ApplicationFormSubmitted>
    {
        public Task Consume(ConsumeContext<ApplicationFormSubmitted> context)
        {
            System.Console.WriteLine($"Application form for {context.Message.Nickname} created.");
            return Task.CompletedTask;
        }
    }
}