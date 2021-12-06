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

        public static Member Accept(ApplicationFormAccepted message)
        {
            DateTime joinDate = DateTime.UtcNow;

            Member member = new Member(message.Nickname, message.Email)
            {
                BiNickname = message.BiNickname,
                DateOfBirth = message.DateOfBirth,
                DiscordId = message.DiscordId,
                GithubNickname = message.GithubNickname,
                Gmail = message.Gmail,
                SteamId = message.SteamId,
                TeamspeakId = message.TeamspeakId,
                JoinDate = joinDate
            };

            Guid recruitmentSagaId = Guid.NewGuid();

            member.Publish(new MemberCreated(message.Nickname,
                                             message.Email,
                                             message.BiNickname,
                                             message.DateOfBirth,
                                             message.DiscordId,
                                             message.GithubNickname,
                                             message.Gmail,
                                             message.SteamId,
                                             message.TeamspeakId,
                                             joinDate,
                                             recruitmentSagaId));

            member.StartRecruitmentWindow(recruitmentSagaId);
            member.StartAssessmentWindow(recruitmentSagaId);
            member.Promote(RankId.Recruit);
            return member;
        }

        public string Nickname { get; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoinDate { get; private set; }

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

        public void Promote(RankId rankId)
        {
            RankId = rankId;
            Publish(new MemberPromoted(Nickname, RankId.Id, DateTime.UtcNow));
        }
    
        public void CheckMods()
        {
            Publish(new ModsChecked(Nickname));
        }

        public void CompleteBootcamp()
        {
            Publish(new BootcampCompleted(Nickname));
        }
    
        private void StartRecruitmentWindow(Guid recruitmentSagaId)
        {
            RecruitmentSettings recruitmentSettings = RecruitmentSettings.Instance;
            DateTime recruitmentWindowEndDate = JoinDate.AddDays(recruitmentSettings.RecruitmentWindowDays);
            Publish(new RecruitTrialExpired(Nickname, recruitmentSettings.RecruitmentWindowDays, recruitmentWindowEndDate, recruitmentSagaId));
        }

        private void StartAssessmentWindow(Guid recruitmentSagaId)
        {
            RecruitmentSettings recruitmentSettings = RecruitmentSettings.Instance;
            DateTime assessmentWindowEndDate = JoinDate.AddDays(recruitmentSettings.ModsAssesmentWindowDays);
            Publish(new RecruitAssessmentExpired(Nickname, recruitmentSettings.ModsAssesmentWindowDays, assessmentWindowEndDate, recruitmentSagaId));
        }        
    }
}