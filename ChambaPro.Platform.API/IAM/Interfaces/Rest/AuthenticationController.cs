using System.Net.Mime;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Authorization;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest;


[EnableCors("AllowSpecificOrigins")]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;

    public AuthenticationController(IUserCommandService userCommandService)
    {
        _userCommandService = userCommandService;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        try
        {
            var result = await _userCommandService.Handle(signInCommand);
            var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler
                .ToResourceFromEntity(result.Item1!, result.Item2);

            return Ok(authenticatedUserResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        try
        {
            var result = await _userCommandService.Handle(signUpCommand);
                
            if (result is null)
                return BadRequest(new { message = "User registration failed" });

            return Ok(new { message = "User registered successfully" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
