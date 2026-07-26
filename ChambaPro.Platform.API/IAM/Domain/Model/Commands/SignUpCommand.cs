namespace ChambaPro.Platform.API.IAM.Domain.Model.Commands;

public record SignUpCommand(
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