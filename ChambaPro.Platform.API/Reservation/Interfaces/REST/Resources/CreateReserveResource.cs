namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

public record CreateReserveResource(
    DateTime Date,
    string Time,
    string Description,
    int ClientId,
    string CategoryId
);
