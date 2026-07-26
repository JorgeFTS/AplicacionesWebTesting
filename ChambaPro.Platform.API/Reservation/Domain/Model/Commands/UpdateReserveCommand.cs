using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

public record UpdateReserveCommand(
    int ReserveId,
    DateTime Date,
    TimeSpan Time,
    string Description,
    int ClientId,
    string CategoryId,
    int? TechnicianId,
    ReservationStatus Status
);