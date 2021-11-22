using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Storage;
using Roster.Web.Security;
using Domain = Roster.Core.Domain;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    [Authorize(Policy = Policy.ViewMembers)]
    public class ListModel : PageModel
    {
        private const int PageSize = 20;

        private readonly IMemberStorage _memberStorage;

        public ListModel(IMemberStorage memberStorage)
        {
            _memberStorage = memberStorage;
        }

        [BindProperty(SupportsGet = true)]
        public string FilterName { get; set; }

        public PaginatedList<Domain.Member> Results { get; private set; }

        public IActionResult OnGet(int pageIndex = 1)
        {
            Results = _memberStorage.Page(new FilterMembers(FilterName), m => m.Nickname, pageIndex, PageSize);
            return Page();
        }

        public IActionResult OnPost(int pageIndex = 1)
        {
            Results = _memberStorage.Page(new FilterMembers(FilterName), m => m.Nickname, pageIndex, PageSize);
            return Page();
        }
    }
}