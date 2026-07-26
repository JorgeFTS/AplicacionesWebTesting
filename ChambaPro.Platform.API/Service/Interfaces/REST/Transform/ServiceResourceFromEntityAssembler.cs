using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Service.Interfaces.REST.Transform;

public class ServiceResourceFromEntityAssembler
{
    public static ServiceResource ToResourceFromEntity(Services entity)
    {
        if (entity == null) return null;

        return new ServiceResource(
            entity.Id,
            entity.ClientId,
            entity.TechnicianId,
            entity.Date,
            entity.Time,
            entity.Description,
            entity.Category,
            entity.Status.ToString(),
            entity.Cost,
            entity.Duration,
            entity.Address
        );
    }
    
    public static IEnumerable<ServiceResource> ToResourceFromEntity(IEnumerable<Services> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}