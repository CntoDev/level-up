namespace Roster.Core.Events
{
    interface IEventSource
    {
        void Register<T>(IEventStore store) where T : class, IEvent;
    }
}