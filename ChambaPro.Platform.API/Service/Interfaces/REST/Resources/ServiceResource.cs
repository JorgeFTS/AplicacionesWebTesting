namespace ChambaPro.Platform.API.Service.Interfaces.REST.Resources;

public record ServiceResource(
    int Id,
    int ClientId,
    int TechnicianId,
    DateTime Date,
    string Time,
    string Description,
    string Category,
    string Status,
    decimal Cost,
    string Duration,
    string Address
);