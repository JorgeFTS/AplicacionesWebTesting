using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

namespace ChambaPro.Platform.API.Reservation.Domain.Services;

public interface IReserveCommandService
{
    Task<Reserve?> Handle(CreateReserveCommand command);
    Task<Reserve?> Handle(UpdateReserveCommand command);
    Task<Reserve?> Handle(DeleteReserveCommand command);
    Task<Reserve?> Handle(CancelReserveCommand command);
}