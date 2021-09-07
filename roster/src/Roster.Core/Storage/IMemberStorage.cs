using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IMemberStorage : IStorage<Member>
    {
        ICollection<string> GetAllNicknames();
    }
}