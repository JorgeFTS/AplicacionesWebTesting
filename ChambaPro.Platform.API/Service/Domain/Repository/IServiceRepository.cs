using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.Service.Domain.Repository;

public interface IServiceRepository : IBaseRepository<Services>
{
    Task<IEnumerable<Services>> FindByClientIdAsync(int clientId);
    Task<IEnumerable<Services>> FindByTechnicianIdAsync(int technicianId);
}