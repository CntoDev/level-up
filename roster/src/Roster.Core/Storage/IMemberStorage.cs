using System.Collections.Generic;

namespace Roster.Core.Storage
{
    public interface IMemberStorage
    {
        public ICollection<string> GetAllNicknames();
    }
}