using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        Users entity, string token)
    {
        return new AuthenticatedUserResource(
            entity.Id,
            entity.Email,
            entity.Name,
            entity.LastName,
            entity.Phone,
            entity.Avatar,
            entity.Type.ToString(),
            token,
            entity.Speciality,
            entity.Description,
            entity.Experience,
            entity.Rating,
            entity.ReviewsCount,
            entity.HourlyRate,
            entity.IsAvailable
        );
    }
}