using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Roster.Core.Commands;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Web.Areas.Roster.Api
{
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly IRankStorage _rankStorage;

        public RankController(IRankStorage rankStorage)
        {
            _rankStorage = rankStorage;
        }

        [HttpGet]
        [Route("ranks")]
        public IActionResult GetRanks()
        {
            return Ok(_rankStorage.All()
                                  .Select(r => new { Id = r.Id.Id, Name = r.Name })
                                  .ToList());
        }

        [HttpPost]
        [Route("ranks/create")]
        public IActionResult Create(CreateRankCommand createRankCommand)
        {
            Rank rank = Rank.Create(createRankCommand.Name);
            _rankStorage.Add(rank);
            _rankStorage.Save();
            return Ok();
        }
    }
}