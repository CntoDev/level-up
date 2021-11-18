using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Roster.Core.Domain;
using Roster.Core.Services;
using Roster.Core.Storage;
using Domain = Roster.Core.Domain;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    public class DetailsModel : PageModel
    {
        private readonly IApplicationStorage _storage;
        private readonly ApplicationFormService _service;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IApplicationStorage storage, ApplicationFormService service, ILogger<DetailsModel> logger)
        {
            _storage = storage;
            _service = service;
            _logger = logger;
        }

        public Domain.ApplicationForm ApplicationForm { get; set; }

        [BindProperty]
        public string Nickname { get; set; }

        [Display(Name = "Interviewer comment")]
        [BindProperty]
        public string InterviewerComment { get; set; }

        public IActionResult OnGet(string nickname)
        {
            Nickname = nickname;
            ApplicationForm = _storage.GetByNickname(new Domain.MemberNickname(nickname));
            return Page();
        }

        public IActionResult OnPostAccept()
        {
            _service.AcceptApplicationForm(new MemberNickname(Nickname));
            return RedirectToPage("/Member/List");
        }

        public IActionResult OnPostReject()
        {
            _service.RejectApplicationForm(new MemberNickname(Nickname), InterviewerComment);
            return RedirectToPage("ListApplications");
        }
    }
}