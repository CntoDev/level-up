namespace Roster.Core.Consumers;

using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

public class WarningsConsumer : 
    IConsumer<ApplicationFormSubmitted>, 
    IConsumer<EnoughEventsAttended>, 
    IConsumer<RecruitAssessmentExpired>, 
    IConsumer<RecruitTrialExpired>
{
    readonly IStorage<Warning> _storage;
    
    public WarningsConsumer(IStorage<Warning> storage)
    {
        _storage = storage;
    }

    public Task Consume(ConsumeContext<ApplicationFormSubmitted> context)
    {
        Warning warning = new(context.Message);
        _storage.Add(warning);
        _storage.Save();
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<EnoughEventsAttended> context)
    {
        Warning warning = new(context.Message);
        _storage.Add(warning);
        _storage.Save();
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<RecruitAssessmentExpired> context)
    {
        Warning warning = new(context.Message);
        _storage.Add(warning);
        _storage.Save();
        return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<RecruitTrialExpired> context)
    {
        Warning warning = new(context.Message);
        _storage.Add(warning);
        _storage.Save();
        return Task.CompletedTask;
    }
}