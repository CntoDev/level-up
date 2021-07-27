using System.Collections.Generic;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class MemoryMemberStorage : IMemberStorage
    {
        public ICollection<string> GetAllNicknames()
        {
            // Stub implementation
            return new List<string>();
        }
    }
}