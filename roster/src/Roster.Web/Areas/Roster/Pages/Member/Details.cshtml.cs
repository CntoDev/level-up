using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roster.Core.Commands;
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

        public IEnumerable<Saga.RecruitmentSaga> RecruitmentSagas { get; set; }

        public bool OneClickAssessment => Domain.RecruitmentSettings.Instance.OneClickAssessment;

        [BindProperty]
        public string Nickname { get; set; }

        public IActionResult OnGet(string nickname)
        {
            Member = _memberStorage.Find(nickname);
            RecruitmentSagas = _processSource.RecruitmentSagas.Where(rs => rs.Nickname.Equals(nickname));
            Nickname = Member.Nickname;
            RankName = _querySource.Ranks.ToList().First(r => r.Id.Equals(Member.RankId)).Name;
            return Page();
        }

        public async Task<IActionResult> OnPostModsCheckedAsync()
        {
            if (OneClickAssessment)
                _memberService.CompleteAssessment(Nickname);
            else
                _memberService.CheckMods(Nickname);

            await Task.Delay(3000);
            RecruitmentSagas = _processSource.RecruitmentSagas.Where(rs => rs.Nickname.Equals(Nickname));
            Member = _memberStorage.Find(Nickname);
            RankName = _querySource.Ranks.ToList().First(r => r.Id.Equals(Member.RankId)).Name;
            return Page();
        }

        public async Task<IActionResult> OnPostBootcampDoneAsync()
        {
            if (OneClickAssessment)
                _memberService.CompleteAssessment(Nickname);
            else
                _memberService.CompleteBootcamp(Nickname);

            await Task.Delay(3000);
            RecruitmentSagas = _processSource.RecruitmentSagas.Where(rs => rs.Nickname.Equals(Nickname));
            Member = _memberStorage.Find(Nickname);
            RankName = _querySource.Ranks.ToList().First(r => r.Id.Equals(Member.RankId)).Name;
            return Page();
        }
    }
}