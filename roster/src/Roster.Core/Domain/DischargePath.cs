using System.ComponentModel.DataAnnotations;

namespace Roster.Core.Domain
{
    public enum DischargePath
    {
        [Display(Name = "Recruitment failed")]
        RecruitmentFailed = 1,

        [Display(Name = "Self discharge")]
        SelfDischarge = 2,

        [Display(Name = "Lack of activity")]
        LackOfActivity = 3,

        [Display(Name = "Breach of regulations")]
        BreachOfRegulations = 4
    }
}