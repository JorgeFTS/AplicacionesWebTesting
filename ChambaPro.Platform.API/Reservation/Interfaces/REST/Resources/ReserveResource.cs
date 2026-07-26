namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

public record ReserveResource(
    int Id,
    DateTime Date,
    string Time,
    string Description,
    int ClientId,
    string ClientName,
    string CategoryId,
    int? TechnicianId,
    string Status
);
