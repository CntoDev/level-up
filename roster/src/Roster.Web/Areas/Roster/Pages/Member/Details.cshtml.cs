using System.Collections.Generic;
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
    [Authorize(Policy = Policy.ViewMembers)]
    public class DetailsModel : PageModel
    {
        private readonly IStorage<Domain.Member> _memberStorage;
        private readonly IQuerySource _querySource;
        private readonly MemberService _memberService;
        private readonly IProcessSource _processSource;

        public DetailsModel(IStorage<Domain.Member> memberStorage, IQuerySource querySource, IProcessSource processSource, MemberService memberService)
        {
            _memberStorage = memberStorage;
            _querySource = querySource;
            _processSource = processSource;
            _memberService = memberService;
        }

        public Domain.Member Member { get; set; }

        public string RankName { get; set; }

        public string DischargeState { get; set; }

        public IEnumerable<Saga.RecruitmentSaga> RecruitmentSagas { get; set; }

        public bool OneClickAssessment => Domain.RecruitmentSettings.Instance.OneClickAssessment;

        public bool AutomaticDischargeEnabled { get; set; }

        public string AutomaticDischargeLabel { get; set; }

        public bool InRecruitment
        {
            get
            {
                var lastRecruitmentSaga = RecruitmentSagas.LastOrDefault();

                if (lastRecruitmentSaga is null)
                    return false;
                else
                    return !lastRecruitmentSaga.IsSagaFinished();
            }
        }

        [BindProperty]
        public string Nickname { get; set; }

        public IActionResult OnGet(string nickname)
        {
            Member = _memberStorage.Find(nickname);
            RecruitmentSagas = _processSource.RecruitmentSagas.Where(rs => rs.Nickname.Equals(nickname));
            Nickname = Member.Nickname;
            RankName = _querySource.Ranks.ToList().First(r => r.Id.Equals(Member.RankId)).Name;
            DischargeState = Member.LastDischarge();

            (AutomaticDischargeEnabled, AutomaticDischargeLabel) = RecruitmentSagas.LastOrDefault()?.AutomaticDischarge switch
            {
                null => (false, ""),
                true => (true, "Switch discharge to manual"),
                false => (true, "Switch discharge to automatic")
            };

            return Page();
        }

        public async Task<IActionResult> OnPostModsCheckedAsync()
        {
            if (OneClickAssessment)
                _memberService.CompleteAssessment(Nickname);
            else
                _memberService.CheckMods(Nickname);

            await Task.Delay(3000);
            return RedirectToPage(new { nickname = Nickname });
        }

        public async Task<IActionResult> OnPostBootcampDoneAsync()
        {
            if (OneClickAssessment)
                _memberService.CompleteAssessment(Nickname);
            else
                _memberService.CompleteBootcamp(Nickname);

            await Task.Delay(3000);
            return RedirectToPage(new { nickname = Nickname });
        }

        public async Task<IActionResult> OnPostToggleAutomaticDischargeAsync()
        {
            _memberService.ToggleAutomaticDischarge(Nickname);
            await Task.Delay(3000);
            return RedirectToPage(new { nickname = Nickname });
        }

        public async Task<IActionResult> OnPostEnoughEventsAttended()
        {
            _memberService.AttendEnoughEvents(Nickname);
            await Task.Delay(3000);
            return RedirectToPage(new { nickname = Nickname });
        }

        public IActionResult OnPostDischarge()
        {
            return RedirectToPage("Discharge", new { nickname = Nickname });
        }

        public IActionResult OnPostRejoin()
        {
            _memberService.Rejoin(Nickname);
            return RedirectToPage(new { nickname = Nickname });
        }
    }
}