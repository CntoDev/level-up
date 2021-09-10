using System;
using System.Linq;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Core.Services
{
    public class MemberService
    {
        private readonly IMemberStorage _memberStorage;
        private readonly IEventStore _eventStore;

        public MemberService(IMemberStorage memberStorage, IEventStore eventStore)
        {
            _memberStorage = memberStorage;
            _eventStore = eventStore;
        }

        public bool VerifyMemberEmail(string email, string code)
        {
            var members = _memberStorage.Search(new MembersByEmail(email));

            if (members.Count() != 1)
                throw new ArgumentException($"Member with address {email} does not exist.");

            Member member = members.First();
            bool isVerified = member.VerifyEmail(code);
            _memberStorage.Save();
            _eventStore.Publish(member.Events());
            
            return isVerified;
        }
    }
}