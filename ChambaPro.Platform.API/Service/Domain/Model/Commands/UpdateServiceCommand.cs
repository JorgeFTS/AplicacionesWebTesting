using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.Service.Domain.Model.Commands;

public record UpdateServiceCommand(
    int Id,
    DateTime Date,
    string Time,
    string Description,
    string Category,
    decimal Cost,
    string Duration,
    string Address,
    ServiceStatus Status
);