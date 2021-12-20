using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Roster.Core.Commands;
using Roster.Core.Domain;
using Roster.Core.Services;
using Roster.Core.Storage;
using System.Linq;

namespace Roster.Web.Areas.Roster.Api
{
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IStorage<Member> _memberStorage;
        private readonly IQuerySource _querySource;
        private readonly MemberService _memberService;

        public MemberController(IStorage<Member> memberStorage, IQuerySource querySource, MemberService memberService)
        {
            _memberStorage = memberStorage;
            _querySource = querySource;
            _memberService = memberService;
        }

        [HttpGet]
        [Route("members")]
        public IActionResult GetMembers()
        {
            var members = from m in _querySource.Members.ToList()
                          join r in _querySource.Ranks.ToList()
                          on m.RankId equals r.Id into rank
                          from item in rank.DefaultIfEmpty()
                          select new
                          {
                              Member = m,
                              Rank = item
                          };

            return Ok(members);
        }

        [HttpPost]
        [Route("members/promote")]
        public IActionResult PromoteMember(PromoteMemberCommand promoteMemberCommand)
        {
            Result result = _memberService.PromoteMember(promoteMemberCommand);

            return result switch
            {
                { IsSuccess: true } => Ok(),
                _ => BadRequest(result.Errors)
            };
        }
    }
}