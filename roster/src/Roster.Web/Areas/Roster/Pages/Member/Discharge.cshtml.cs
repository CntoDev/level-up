using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Services;
using Roster.Core.Storage;
using Roster.Web.Security;
using Domain = Roster.Core.Domain;
using Saga = Roster.Core.Sagas;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    public class DischargeModel : PageModel
    {
        readonly MemberService _service;

        public DischargeModel(MemberService service)
        {
            _service = service;
        }

        [BindProperty]
        public string Nickname { get; set; }

        [Display(Name = "Discharge path")]
        [BindProperty]
        public Domain.DischargePath DischargePath { get; set; }

        [Display(Name = "Comment")]
        [BindProperty]
        public string Comment { get; set; }

        public IActionResult OnGet(string nickname)
        {
            Nickname = nickname;
            return Page();
        }

        public IActionResult OnPost()
        {
            _service.Discharge(Nickname, DischargePath, Comment);
            return RedirectToPage("List");
        }
    }
}