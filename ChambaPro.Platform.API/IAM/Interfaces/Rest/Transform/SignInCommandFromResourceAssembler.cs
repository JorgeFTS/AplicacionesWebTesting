using ChambaPro.Platform.API.IAM.Domain.Model.Commands;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(
            resource.Email,
            resource.Password,
            resource.UserType
        );
    }
}