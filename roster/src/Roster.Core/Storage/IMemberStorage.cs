using System.Collections.Generic;

namespace Roster.Core.Storage
{
    interface IMemberStorage
    {
        public ICollection<string> GetAllNicknames();
    }
}