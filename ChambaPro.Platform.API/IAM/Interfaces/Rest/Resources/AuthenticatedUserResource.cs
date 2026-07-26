namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(
    int Id,
    string Email,
    string Name,
    string Lastname,
    string Phone,
    string Avatar,
    string UserType,
    string Token,
    string? Speciality = null,
    string? Description = null,
    string? Experience = null,
    decimal? Rating = null,
    int? ReviewsCount = null,
    decimal? HourlyRate = null,
    bool? IsAvailable = null
);