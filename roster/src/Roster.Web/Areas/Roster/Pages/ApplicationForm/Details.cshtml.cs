using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Storage;
using Domain = Roster.Core.Domain;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    public class DetailsModel : PageModel
    {
        private readonly IApplicationStorage _storage;

        public DetailsModel(IApplicationStorage storage)
        {
            _storage = storage;
        }

        public Domain.ApplicationForm ApplicationForm { get; set; }

        public IActionResult OnGet(string nickname)
        {
            ApplicationForm = _storage.GetByNickname(nickname);
            return Page();
        }
    }
}