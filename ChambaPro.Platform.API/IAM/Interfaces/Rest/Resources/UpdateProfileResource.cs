namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

public record UpdateProfileResource(
    string Name,
    string Lastname,
    string Phone,
    string? Avatar = null
);