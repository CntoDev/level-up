using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseMemberStorage : IMemberStorage
    {
        private readonly RosterDbContext _rosterDbContext;

        public DatabaseMemberStorage(RosterDbContext rosterDbContext)
        {
            _rosterDbContext = rosterDbContext;
        }

        public void Add(Member member)
        {
            _rosterDbContext.Add(member);
        }

        public Member Find(object key)
        {
            return _rosterDbContext.Members.Find(key);
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

        public void Remove(Member member)
        {
            _rosterDbContext.Members.Remove(member);
        }

        public void Save()
        {
            _rosterDbContext.SaveChanges();
        }

        public IEnumerable<Member> Search(ISpecification<Member> specification)
        {
            return _rosterDbContext.Members.Where(specification.Predicate);
        }
    }
}