using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.Reservation.Domain.Repositories;

public interface IReserveRepository : IBaseRepository<Reserve>
    
{
    Task<IEnumerable<Reserve>> FindByClientIdAsync(int clientId);
    Task<IEnumerable<Reserve>> FindByTechnicianIdAsync(int technicianId);
}