namespace ChambaPro.Platform.API.Service.Interfaces.REST.Resources;

public record CreateServiceResource(
    int ClientId,
    int TechnicianId,
    DateTime Date,
    string Time,
    string Description,
    string Category,
    decimal Cost,
    string Duration,
    string Address
);