using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Roster.Core.Services;
using Roster.Core.Commands;
using System.Collections.Generic;
using Roster.Core.Domain;
using Roster.Web.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Roster.Core.Storage;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    [AllowAnonymous]
    public class ApplyModel : PageModel
    {
        private string[] _dlcNames;
        private readonly ApplicationFormService _rosterCoreService;
        private readonly IQuerySource _querySource;
        private readonly ILogger<ApplyModel> _logger;
        private ICollection<OwnedDlc> _ownedDlcs;

        public ApplyModel(
            ApplicationFormService rosterCoreService,
            IQuerySource querySource,
            ILogger<ApplyModel> logger)
        {
            _rosterCoreService = rosterCoreService;
            _querySource = querySource;
            _logger = logger;
            _ownedDlcs = new List<OwnedDlc>();
        }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Bohemia Interactive nickname")]
        [BindProperty]
        public string BiNickname { get; set; }

        [Display(Name = "Discord ID")]
        [BindProperty]
        public string DiscordId { get; set; }

        [Display(Name = "GitHub nickname")]
        [BindProperty]
        public string GithubNickname { get; set; }

        [Display(Name = "Google e-mail")]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string Gmail { get; set; }

        [Display(Name = "Steam username")]
        [BindProperty]
        public string SteamId { get; set; }

        [Display(Name = "Teamspeak ID")]
        [BindProperty]
        public string TeamspeakId { get; set; }

        [Display(Name = "Owned DLCs")]
        [BindProperty]
        public IEnumerable<string> OwnedDlcs
        {
            get
            {
                return _ownedDlcs.Select(dlc => dlc.Name);
            }
            set
            {
                _ownedDlcs = value.Select(x => new OwnedDlc() { Name = x })
                                  .ToList();
            }
        }

        public List<SelectListItem> Dlcs { get; set; }

        public List<SelectListItem> Pronouns { get; set; }

        [Display(Name = "Preferred pronouns")]
        [BindProperty]
        public string PreferredPronouns { get; set; }

        public List<SelectListItem> TimeZones { get; set; }

        [Display(Name = "Time zone")]
        [BindProperty]
        public string TimeZone { get; set; }

        public List<SelectListItem> LanguageSkillLevels { get; set; }

        [Display(Name = "Language skill level")]
        [BindProperty]
        public string LanguageSkillLevel { get; set; }

        [Display(Name = "How much experience do you have playing with Arma series?")]
        [BindProperty]
        public string PreviousArmaExperience { get; set; }

        [Display(Name = "Which Arma mods have you been using so far?")]
        [BindProperty]
        public string PreviousArmaModExperience { get; set; }

        [Display(Name = "Where do you see yourself in the community, any specific roles or activities?")]
        [BindProperty]
        public string DesiredCommunityRole { get; set; }

        [Display(Name = "Tell us a little about yourself.")]
        [BindProperty]
        public string AboutYourself { get; set; }

        public IActionResult OnGet()
        {
            DateOfBirth = DateTime.UtcNow.Date;
            FetchLinkedData();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ApplicationFormCommand formCommand = new()
                {
                    Nickname = Nickname,
                    DateOfBirth = DateOfBirth,
                    Email = Email,
                    BiNickname = BiNickname,
                    DiscordId = DiscordId,
                    GithubNickname = GithubNickname,
                    Gmail = Gmail,
                    OwnedDlcs = _ownedDlcs,
                    SteamId = SteamId,
                    TeamspeakId = TeamspeakId,
                    PreferredPronouns = int.Parse(PreferredPronouns),
                    TimeZone = TimeZone,
                    LanguageSkillLevel = int.Parse(LanguageSkillLevel),
                    PreviousArmaExperience = PreviousArmaExperience,
                    PreviousArmaModExperience = PreviousArmaModExperience,
                    DesiredCommunityRole = DesiredCommunityRole,
                    AboutYourself = AboutYourself
                };

                _logger.LogInformation("Submitting application form {@command}", formCommand);
                var result = _rosterCoreService.SubmitApplicationForm(formCommand);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Application form success.");
                    return RedirectToPage("ApplicationFormCreated", new { nickname = Nickname });
                }
                else
                {
                    _logger.LogError("Application form submit failed with error {errors}", result.ToString());
                    ModelState.AddResultErrors(result);
                }

            }

            FetchLinkedData();
            return Page();
        }

        private void FetchLinkedData()
        {
            _dlcNames = _querySource.Dlcs.Select(dlc => dlc.DlcName.Name).ToArray();
            Dlcs = _dlcNames.Select(x => new SelectListItem(x, x)).ToList();
            Pronouns = Enum.GetValues<Pronoun>().Select(x => new SelectListItem(x.ToString(), ((int)x).ToString())).ToList();
            TimeZones = TimeZoneInfo.GetSystemTimeZones().Select(x => new SelectListItem(x.DisplayName, x.Id)).ToList();
            LanguageSkillLevels = Enum.GetValues<LanguageSkillLevel>().Select(x => new SelectListItem(x.ToString(), ((int)x).ToString())).ToList();
        }
    }
}