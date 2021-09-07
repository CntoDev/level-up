using System.Collections.Generic;

namespace Roster.Core.Events
{
    interface IEventSource
    {
        IEnumerable<IEvent> Events();
    }
}