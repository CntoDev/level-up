using System;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class Member : AggregateRoot
    {
        private string _verificationCode;
        private DateTime? _verificationTime;
        private bool _emailVerified;

        private Member(string nickname, string email)
        {
            Nickname = nickname;
            Email = email;
        }

        public static Member Accept(ApplicationFormAccepted message)
        {
            DateTime joinDate = DateTime.UtcNow;

            Member member = new(message.Nickname, message.Email)
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

        public DateTime DateOfBirth { get; private init; }

        public DateTime JoinDate { get; init; }

        public string Email { get; }

        public string BiNickname { get; init; }

        public string SteamId { get; init; }

        public string Gmail { get; init; }

        public string GithubNickname { get; init; }

        public string DiscordId { get; init; }

        public string TeamspeakId { get; init; }

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
                _emailVerified = true;
                Publish(new MemberEmailVerified(Nickname));
                return true;
            }

            return false;
        }

        public void ToggleAutomaticDischarge()
        {
            Publish(new AutomaticDischargeToggled(Nickname));
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