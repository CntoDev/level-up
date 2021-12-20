using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Services;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    public class VerifyModel : PageModel
    {
        private readonly MemberService _service;

        public VerifyModel(MemberService memberService)
        {
            _service = memberService;
        }

        public string Email { get; set; }

        public IActionResult OnGet(string email, string code)
        {
            bool isVerified = _service.VerifyMemberEmail(email, code);

            if (isVerified)
            {
                Email = email;
                return Page();
            }
            else
                return Unauthorized();
        }
    }
}