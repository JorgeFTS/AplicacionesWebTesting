namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

public record SignUpResource(
    string Email,
    string Password,
    string Name,
    string Lastname,
    string Phone,
    string UserType,
    string? Speciality = null,
    string? Description = null,
    string? Experience = null,
    decimal? HourlyRate = null
);