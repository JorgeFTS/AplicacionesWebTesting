namespace ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;

public record UpdateTechnicianProfileResource(
    string Speciality,
    string Description,
    string Experience,
    decimal HourlyRate,
    bool IsAvailable
);