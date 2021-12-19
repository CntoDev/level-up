namespace Roster.Core.Domain
{
    public class RecruitmentSettings
    {
        public static RecruitmentSettings Instance { get; private set; }
        public RecruitmentSettings()
        {
            Instance = this;
        }
        public int RecruitmentWindowDays { get; set; }
        public int ModsAssesmentWindowDays { get; set; }
        public int MinimalAttendance { get; set; }
        public bool OneClickAssessment { get; set; }
        public bool SendMail { get; set; }
    }
}