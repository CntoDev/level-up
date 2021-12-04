using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Storage;
using Roster.Web.Security;
using Domain = Roster.Core.Domain;

namespace Roster.Web.Areas.Roster.Pages.ApplicationForm
{
    [Authorize(Policy = Policy.ViewApplications)]
    public class ListApplicationsModel : PageModel
    {
        private readonly IStorage<Domain.ApplicationForm> _storage;

        public ListApplicationsModel(IStorage<Domain.ApplicationForm> storage)
        {
            _storage = storage;
        }

        public IEnumerable<Domain.ApplicationForm> ApplicationForms { get; set; }

        public IActionResult OnGet()
        {
            ApplicationForms = _storage.All();
            return Page();
        }
    }
}