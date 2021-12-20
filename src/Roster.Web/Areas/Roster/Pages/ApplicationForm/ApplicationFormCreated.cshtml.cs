using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    public class Created : PageModel
    {
        public string Nickname { get; set; }
        
        public IActionResult OnGet(string nickname)
        {
            Nickname = nickname;
            return Page();
        }
    }
}