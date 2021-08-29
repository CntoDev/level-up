namespace Roster.Core.Events {
    public interface IEventStore {
        void Publish<T>(T @event) where T: class;
    }
}