using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Commands;
using ChambaPro.Platform.API.Service.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Service.Interfaces.REST.Transform;

public class UpdateServiceCommandFromResourceAssembler
{
    public static UpdateServiceCommand ToCommandFromResource(int id, UpdateServiceResource resource)
    {
        if (!Enum.TryParse<ServiceStatus>(resource.Status, out var status))
        {
            throw new ArgumentException($"Invalid status value: {resource.Status}");
        }

        return new UpdateServiceCommand(
            id,
            resource.Date,
            resource.Time,
            resource.Description,
            resource.Category,
            resource.Cost,
            resource.Duration,
            resource.Address,
            status
        );
    }
}