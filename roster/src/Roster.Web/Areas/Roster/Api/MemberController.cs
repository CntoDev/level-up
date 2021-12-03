using System;
using System.Collections.Generic;
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
        private readonly IMemberStorage _memberStorage;
        private readonly IRankStorage _rankStorage;
        private readonly MemberService _memberService;

        public MemberController(IMemberStorage memberStorage, IRankStorage rankStorage, MemberService memberService)
        {
            _memberStorage = memberStorage;
            _rankStorage = rankStorage;
            _memberService = memberService;
        }

        [HttpGet]
        [Route("members")]
        public IActionResult GetMembers()
        {
            var members = from m in _memberStorage.All().ToList()
                          join r in _rankStorage.All().ToList()
                          on m.RankId equals r.Id into rank
                          from item in rank.DefaultIfEmpty()
                          select new
                          {
                              Member = m,
                              Rank = new
                              {
                                  Id = item?.Id.Id,
                                  Name = item?.Name
                              }
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