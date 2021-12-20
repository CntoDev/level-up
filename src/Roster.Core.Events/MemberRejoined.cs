using MassTransit;

namespace Roster.Core.Events
{
    public record MemberRejoined (string Nickname, bool Alumni, Guid CorrelationId) : IEvent, CorrelatedBy<Guid>;
}