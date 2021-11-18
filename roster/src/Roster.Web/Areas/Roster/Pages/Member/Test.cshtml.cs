using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    public class TestModel : PageModel
    {
        private readonly IApplicationStorage _applicationStorage;
        private readonly IEventStore _eventStore;
        private readonly ILogger<TestModel> _logger;
        
        public TestModel(IApplicationStorage applicationStorage, IEventStore eventStore, ILogger<TestModel> logger)
        {
            _applicationStorage = applicationStorage;
            _eventStore = eventStore;
            _logger = logger;
        }

        [BindProperty]
        public string Nickname { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("Accepting form for {nickname}", Nickname);
            var applicationForm = _applicationStorage.GetByNickname(new MemberNickname(Nickname));
            var applicationFormAccepted = new ApplicationFormAccepted(applicationForm);
            _eventStore.Publish(applicationFormAccepted);
            return Page();
        }
    }
}