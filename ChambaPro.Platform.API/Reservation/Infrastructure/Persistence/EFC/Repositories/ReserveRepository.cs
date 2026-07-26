using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Reservation.Infrastructure.Persistence.EFC.Repositories;

public class ReserveRepository(AppDbContext context)
    : BaseRepository<Reserve>(context), IReserveRepository
{
    public async Task<IEnumerable<Reserve>> FindByClientIdAsync(int clientId)
    {
        return await Context.Set<Reserve>()
            .Where(r => r.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reserve>> FindByTechnicianIdAsync(int technicianId)
    {
        return await Context.Set<Reserve>()
            .Where(r => r.TechnicianId == technicianId)
            .ToListAsync();
    }
}