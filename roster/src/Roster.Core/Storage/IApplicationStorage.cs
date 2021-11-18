using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IApplicationStorage
    {
        void StoreApplicationForm(ApplicationForm form);
        void Save();
        IEnumerable<ApplicationForm> GetAll();
        ApplicationForm GetByNickname(MemberNickname nickname);
    }
}