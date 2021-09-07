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

        public void Remove(Member member)
        {
            _rosterDbContext.Members.Remove(member);
        }

        public void Save()
        {
            _rosterDbContext.SaveChanges();
        }
    }
}