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
        private readonly IStorage<Member> _memberStorage;
        private readonly IQuerySource _querySource;
        private readonly IEventStore _eventStore;

        public MemberService(IStorage<Member> memberStorage, IQuerySource querySource, IEventStore eventStore)
        {
            _memberStorage = memberStorage;
            _querySource = querySource;
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
                RankId rankId = new RankId(promoteMemberCommand.RankId);
                Rank rank = _querySource.Ranks.ToList().First(r => r.Id.Equals(rankId));
                
                member.Promote(rank.Id);
                _memberStorage.Save();
                _eventStore.Publish(member.Events());
                
                return Result.Ok();
            } catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public void AcceptMember(ApplicationFormAccepted message)
        {
            Member member = Member.Accept(message);
            _memberStorage.Add(member);
            _memberStorage.Save();
            _eventStore.Publish(member.Events());
        }

        public void CheckMods(CheckModsCommand command)
        {
            Member member = _memberStorage.Find(command.Nickname);
            member.CheckMods();
            _memberStorage.Save();
            _eventStore.Publish(member.Events());
        }

        public void CompleteBootcamp(CompleteBootcampCommand command)
        {
            Member member = _memberStorage.Find(command.Nickname);
            member.CompleteBootcamp();
            _memberStorage.Save();
            _eventStore.Publish(member.Events());
        }
    }
}