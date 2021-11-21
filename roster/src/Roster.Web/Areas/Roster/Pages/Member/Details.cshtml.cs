using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Storage;
using Roster.Web.Security;
using Domain = Roster.Core.Domain;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    [Authorize(Policy = Policy.ViewMembers)]
    public class DetailsModel : PageModel
    {
        private readonly IMemberStorage _memberStorage;

        public DetailsModel(IMemberStorage memberStorage)
        {
            _memberStorage = memberStorage;
        }
        
        public Domain.Member Member { get; set; }

        public IActionResult OnGet(string nickname)
        {
            Member = _memberStorage.Find(nickname);
            return Page();
        }
    }
}