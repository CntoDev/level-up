using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roster.Core.Commands;
using Roster.Core.Domain;
using Roster.Core.Services;
using Roster.Core.Storage;

namespace Roster.Web.Areas.Roster.Api
{
    [ApiController]
    public class ApplicationFormController : ControllerBase
    {
        private readonly IStorage<ApplicationForm> _applicationStorage;
        private readonly ILogger<ApplicationFormController> _logger;
        private readonly ApplicationFormService _applicationService;

        public ApplicationFormController(IStorage<ApplicationForm> applicationStorage, ILogger<ApplicationFormController> logger, ApplicationFormService service)
        {
            _applicationStorage = applicationStorage;
            _logger = logger;
            _applicationService = service;
        }

        [HttpGet]
        [Route("application-forms")]
        public IEnumerable<ApplicationForm> GetApplicationForms()
        {
            return _applicationStorage.All();
        }

        [HttpGet]
        [Route("application-forms/{nickname}")]
        public ActionResult<ApplicationForm> GetApplicationForm(string nickname)
        {
            try
            {
                ApplicationForm form = _applicationStorage.QueryOne(f => f.Nickname.Equals(new MemberNickname(nickname)));

                if (form != null)
                    return Ok(form);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Application form get failed for {nickname}.", nickname);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("application-forms/submit")]
        public IActionResult SubmitApplicationForm([FromBody]ApplicationFormCommand command)
        {
            var result = _applicationService.SubmitApplicationForm(command);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetApplicationForm), new {nickname = command.Nickname}, command);

            return Problem(result.ToString());
        }

        [HttpPost]
        [Route("application-forms/accept")]
        public IActionResult AcceptApplicationForm(AcceptApplicationFormCommand command)
        {
            _applicationService.AcceptApplicationForm(new MemberNickname(command.Nickname));
            return Ok();
        }

        [HttpPost]
        [Route("application-forms/reject")]
        public IActionResult RejectApplicationForm(RejectApplicationFormCommand command)
        {
            _applicationService.RejectApplicationForm(new MemberNickname(command.Nickname), command.Reason);
            return Ok();
        }        
    }
}