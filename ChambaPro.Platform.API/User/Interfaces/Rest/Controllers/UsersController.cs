using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Chambapro.Platform.API.User.Application.Internal.QueryServices;
using Chambapro.Platform.API.User.Application.Internal.CommandServices;
using Chambapro.Platform.API.User.Interfaces.Rest.Resources;

namespace Chambapro.Platform.API.User.Interfaces.Rest.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserQueryService _queryService;
        private readonly UserCommandService _commandService;

        public UsersController(UserQueryService queryService, UserCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized();

            var profile = await _queryService.HandleAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] SaveUserProfileResource resource)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
            if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized();

            var updated = await _commandService.HandleCreateOrUpdateAsync(userId, resource);
            return Ok(updated);
        }
    }
}
