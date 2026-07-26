using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Queries;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Services;

namespace ChambaPro.Platform.API.Reservation.Application.Internal.QueryServices;

public class ReserveQueryService(IReserveRepository repository) : IReserveQueryService
{
    public async Task<IEnumerable<Reserve>> Handle(GetAllReservesQuery query)
    {
        return await repository.ListAsync();
    }

    public async Task<Reserve?> Handle(GetReserveByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Reserve>> Handle(GetReservesByClientIdQuery query)
    {
        return await repository.FindByClientIdAsync(query.ClientId);
    }

    public async Task<IEnumerable<Reserve>> Handle(GetReservesByTechnicianIdQuery query)
    {
        return await repository.FindByTechnicianIdAsync(query.TechnicianId);
    }
}