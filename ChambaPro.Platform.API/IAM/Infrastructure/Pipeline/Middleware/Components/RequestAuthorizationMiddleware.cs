using ChambaPro.Platform.API.IAM.Domain.Model.Queries;
using ChambaPro.Platform.API.IAM.Domain.Services;

namespace ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

        var userId = tokenService.ValidateToken(token);

        if (userId != null)
        {
            var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
            var user = await userQueryService.Handle(getUserByIdQuery);
            context.Items["User"] = user;
        }

        await _next(context);
    }
}