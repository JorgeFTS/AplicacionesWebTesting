using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Service.Domain.Service;

namespace ChambaPro.Platform.API.Service.Application.QueryServices;

public class ServiceQueryService(IServiceRepository repository) : IServiceQueryService
{
    public async Task<IEnumerable<Services>> Handle(GetAllServicesQuery query)
    {
        return await repository.ListAsync();
    }

    public async Task<Services?> Handle(GetServiceByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Services>> Handle(GetServicesByClientIdQuery query)
    {
        return await repository.FindByClientIdAsync(query.ClientId);
    }

    public async Task<IEnumerable<Services>> Handle(GetServicesByTechnicianIdQuery query)
    {
        return await repository.FindByTechnicianIdAsync(query.TechnicianId);
    }
}