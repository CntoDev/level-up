using System;

namespace Roster.Core.Events
{
    public record EmailChallenged(string Nickname, string VerificationCode, DateTime? VerificationTime) : IEvent;
}