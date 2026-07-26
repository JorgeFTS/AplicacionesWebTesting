using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Service.Infrastructure.Persistence.EFC.Repositories;

public class ServiceRepository(AppDbContext context)
    : BaseRepository<Services>(context), IServiceRepository
{
    public async Task<IEnumerable<Services>> FindByClientIdAsync(int clientId)
    {
        return await Context.Set<Services>()
            .Where(s => s.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Services>> FindByTechnicianIdAsync(int technicianId)
    {
        return await Context.Set<Services>()
            .Where(s => s.TechnicianId == technicianId)
            .ToListAsync();
    }
}