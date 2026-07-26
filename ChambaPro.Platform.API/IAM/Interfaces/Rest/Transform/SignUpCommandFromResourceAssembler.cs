using ChambaPro.Platform.API.IAM.Domain.Model.Commands;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Email,
            resource.Password,
            resource.Name,
            resource.Lastname,
            resource.Phone,
            resource.UserType,
            resource.Speciality,
            resource.Description,
            resource.Experience,
            resource.HourlyRate
        );
    }
}