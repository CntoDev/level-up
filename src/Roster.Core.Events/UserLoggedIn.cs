namespace Roster.Core.Events;

public record UserLoggedIn(string Email) : IEvent;