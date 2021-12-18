using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class Member : AggregateRoot
    {
        private string _verificationCode;
        private DateTime? _verificationTime;
        private bool _emailVerified;
        private List<MemberDischarge> _memberDischarges;

        private Member(string nickname, string email)
        {
            Nickname = nickname;
            Email = email;
            _memberDischarges = new();
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

        public bool Discharged { get; private set; }

        public bool EmailVerified => _emailVerified;

        public IEnumerable<MemberDischarge> MemberDischarges => _memberDischarges;

        public string LastDischarge()
        {
            if (_memberDischarges is null)
                return string.Empty;

            var memberDischarge = _memberDischarges.LastOrDefault();
            
            if (memberDischarge is MemberDischarge)
                return $"Discharged on {memberDischarge.DateOfDischarge} for {memberDischarge.DischargePath}.";
            else
                return string.Empty;
        }

        public void ChallengeEmail(string verificationCode)
        {
            _verificationCode = verificationCode;
            _verificationTime = DateTime.UtcNow;

            Publish(new EmailChallenged(Nickname, _verificationCode, _verificationTime));
        }

        public bool VerifyEmail(string verificationCode)
        {
            // 1 hour verification time
            bool isVerified = verificationCode.Equals(_verificationCode) && DateTime.UtcNow.AddHours(-1) < _verificationTime;

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

        public void DischargeRecruit(string reason)
        {
            Discharge(DischargePath.RecruitmentFailed, reason);
        }

        public void Discharge(DischargePath dischargePath, string comment)
        {
            if (Discharged)
                throw new InvalidOperationException("Member already discharged.");
            
            Discharged = true;
            DateTime dischargeDate = DateTime.UtcNow;
            _memberDischarges.Add(new MemberDischarge(dischargeDate, dischargePath, comment));

            Publish(new MemberDischarged(Nickname, dischargeDate, (int)dischargePath, comment));
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