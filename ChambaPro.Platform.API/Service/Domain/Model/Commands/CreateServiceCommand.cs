namespace ChambaPro.Platform.API.Service.Domain.Model.Commands;

public record CreateServiceCommand(
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