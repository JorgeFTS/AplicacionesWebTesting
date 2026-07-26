namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

public record UpdateReserveResource(
    int Id,
    DateTime Date,
    string Time,
    string Description,
    int ClientId,
    string CategoryId,
    int? TechnicianId,
    string Status
);