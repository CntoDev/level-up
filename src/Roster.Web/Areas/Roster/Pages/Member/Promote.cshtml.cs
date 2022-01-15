using Microsoft.AspNetCore.Mvc;
using Domain = Roster.Core.Domain;
using Roster.Core.Storage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.Extensions.Logging;
using Roster.Core.Services;

namespace Roster.Web.Areas.Roster.Pages.Member;

public class PromoteModel : PageModel
{
    readonly IStorage<Domain.Member> _memberStorage;
    readonly IStorage<Domain.Rank> _rankStorage;
    readonly MemberService _memberService;
    readonly ILogger<PromoteModel> _logger;

    public PromoteModel(IStorage<Domain.Member> memberStorage, IStorage<Domain.Rank> rankStorage, MemberService memberService, ILogger<PromoteModel> logger)
    {
        _memberStorage = memberStorage;
        _rankStorage = rankStorage;
        _memberService = memberService;
        _logger = logger;
    }

    [BindProperty]
    public int RankId { get; set; }

    [BindProperty]
    public string Nickname { get; set; }

    public List<SelectListItem> AvailableRanks { get; set; }

    public IActionResult OnGet(string nickname)
    {
        Nickname = nickname;
        var member = _memberStorage.Find(nickname);
        RankId = member.RankId.Id;
        AvailableRanks = _rankStorage.All().Select(x => new SelectListItem(x.Name, x.Id.Id.ToString(), x.Id.Id == RankId)).ToList();

        return Page();
    }

    public IActionResult OnPost()
    {
        _memberService.PromoteMember(new(Nickname, RankId));
        string rankName = _rankStorage.Find(RankId).Name;
        _logger.LogInformation("Member {nickname} promoted to {rank}.", Nickname, rankName);
        
        return RedirectToPage("Details", new { nickname = Nickname });
    }
}