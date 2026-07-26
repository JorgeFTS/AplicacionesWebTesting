using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Queries;

namespace ChambaPro.Platform.API.Reservation.Domain.Services;

public interface IReserveQueryService
{
    Task<IEnumerable<Reserve>> Handle(GetAllReservesQuery query);
    Task<Reserve?> Handle(GetReserveByIdQuery query);
    Task<IEnumerable<Reserve>> Handle(GetReservesByClientIdQuery query);
    Task<IEnumerable<Reserve>> Handle(GetReservesByTechnicianIdQuery query);
}