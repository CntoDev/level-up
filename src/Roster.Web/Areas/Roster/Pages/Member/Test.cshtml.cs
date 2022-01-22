using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Domain = Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Web.Areas.Roster.Pages.Member
{
    public class TestModel : PageModel
    {
        private readonly IStorage<Domain.ApplicationForm> _applicationStorage;
        private readonly IEventStore _eventStore;
        private readonly ILogger<TestModel> _logger;
        
        public TestModel(IStorage<Domain.ApplicationForm> applicationStorage, IEventStore eventStore, ILogger<TestModel> logger)
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
            var applicationForm = _applicationStorage.QueryOne(f => f.Nickname.Equals(new Domain.MemberNickname(Nickname)));
            var applicationFormAccepted = Domain.ApplicationForm.BuildEvent(applicationForm);
            _eventStore.PublishAsync(applicationFormAccepted);
            return Page();
        }
    }
}