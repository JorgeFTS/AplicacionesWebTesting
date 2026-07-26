using ChambaPro.Platform.API.Service.Domain.Model.Commands;
using ChambaPro.Platform.API.Service.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Service.Interfaces.REST.Transform;

public class CreateServiceCommandFromResourceAssembler
{
    public static CreateServiceCommand ToCommandFromResource(CreateServiceResource resource)
    {
        return new CreateServiceCommand(
            resource.ClientId,
            resource.TechnicianId,
            resource.Date,
            resource.Time,
            resource.Description,
            resource.Category,
            resource.Cost,
            resource.Duration,
            resource.Address
        );
    }
}