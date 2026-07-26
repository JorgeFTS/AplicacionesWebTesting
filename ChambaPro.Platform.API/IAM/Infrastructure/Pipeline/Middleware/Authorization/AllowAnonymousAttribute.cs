namespace ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}