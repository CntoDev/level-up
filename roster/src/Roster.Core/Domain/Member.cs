using System;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class Member : AggregateRoot
    {
        private string _verificationCode;
        private DateTime? _verificationTime;
        private bool _emailVerified;

        public Member(string nickname, string email)
        {
            Nickname = nickname;
            Email = email;
        }

        public string Nickname { get; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; }

        public string BiNickname { get; set; }

        public string SteamId { get; set; }

        public string Gmail { get; set; }

        public string GithubNickname { get; set; }

        public string DiscordId { get; set; }

        public string TeamspeakId { get; set; }

        public RankId RankId { get; private set; }

        public bool EmailVerified => _emailVerified;

        public void ChallengeEmail(string verificationCode)
        {
            _verificationCode = verificationCode;
            _verificationTime = DateTime.Now;

            Publish(new EmailChallenged(Nickname, _verificationCode, _verificationTime));
        }

        public bool VerifyEmail(string verificationCode)
        {
            // 1 hour verification time
            bool isVerified = verificationCode.Equals(_verificationCode) && DateTime.Now.AddHours(-1) < _verificationTime;

            if (isVerified)
            {
                _emailVerified = isVerified;
                Publish(new MemberEmailVerified(Nickname));
                return true;
            }

            return false;
        }

        public void Promote(object recruit)
        {
            throw new NotImplementedException();
        }

        public void Promote(RankId rankId)
        {
            RankId = rankId;
            Publish(new MemberPromoted(Nickname, RankId.Id));
        }
    }
}