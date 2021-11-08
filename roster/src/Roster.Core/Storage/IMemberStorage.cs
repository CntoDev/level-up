using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IMemberStorage : IStorage<Member>
    {
        ICollection<string> GetAllNicknames();
        PaginatedList<Member> Page(ISpecification<Member> member, Func<Member, object> orderKeySelector, int page, int pageSize);
    }
}