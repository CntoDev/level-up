using System.Collections.Generic;

namespace Roster.Core.Storage
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