using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Roster.Core.Services;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    [AllowAnonymous]
    public class ApplyModel : PageModel
    {
        private readonly ApplicationService _rosterCoreService;
        private readonly ILogger<ApplyModel> _logger;

        public ApplyModel(ApplicationService rosterCoreService, ILogger<ApplyModel> logger)
        {
            _rosterCoreService = rosterCoreService;
            _logger = logger;
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

        public IActionResult OnGet()
        {
            DateOfBirth = DateTime.Now.Date;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("ApplicationFormCreated", new { nickname = Nickname });
            }

            return Page();
        }
    }
}