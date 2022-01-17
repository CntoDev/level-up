using System.ComponentModel.DataAnnotations;

namespace Roster.Core.Domain
{
    public enum Pronoun
    {
        [Display(Name = "He/Him")]
        He = 0,
        [Display(Name = "She/Her")]
        She = 1,
        [Display(Name = "They/Them")]
        They = 2, // how convinient :P
        Other = 3
    }
}
