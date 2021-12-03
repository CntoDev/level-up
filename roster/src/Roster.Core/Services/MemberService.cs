using System;
using System.Linq;
using FluentResults;
using Roster.Core.Commands;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Core.Services
{
    public class MemberService
    {
        private readonly IMemberStorage _memberStorage;
        private readonly IRankStorage _rankStorage;
        private readonly IEventStore _eventStore;

        public MemberService(IMemberStorage memberStorage, IRankStorage rankStorage, IEventStore eventStore)
        {
            _memberStorage = memberStorage;
            _rankStorage = rankStorage;
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

        public Result PromoteMember(PromoteMemberCommand promoteMemberCommand)
        {
            try {
                Member member = _memberStorage.Find(promoteMemberCommand.Nickname);
                Rank rank = _rankStorage.Find(promoteMemberCommand.RankId);
                member.Promote(rank.Id);
                _memberStorage.Save();
                _eventStore.Publish(member.Events());
                
                return Result.Ok();
            } catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}