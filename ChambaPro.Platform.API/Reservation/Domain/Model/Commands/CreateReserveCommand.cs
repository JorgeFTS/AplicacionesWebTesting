namespace ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

public record CreateReserveCommand(
    DateTime Date,
    TimeSpan Time,
    string Description,
    int ClientId,
    string CategoryId
);
