using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseMemberStorage: Storage<Member>, IMemberStorage
    {
        public DatabaseMemberStorage(RosterDbContext rosterDbContext) : base(rosterDbContext)
        {
        }

        public ICollection<string> GetAllNicknames()
        {
            return _rosterDbContext.Members.Select(m => m.Nickname).ToList();
        }

        public PaginatedList<Member> Page(ISpecification<Member> filter, Func<Member, object> orderKeySelector, int page, int pageSize)
        {
            var filteredMembers = Search(filter);
            var orderedMembers = filteredMembers.OrderBy(orderKeySelector).AsQueryable();

            return PaginatedList<Member>.Create(orderedMembers, page, pageSize);
        }
    }
}