using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;

public static class UpdateReserveCommandFromResourceAssembler
{
    public static UpdateReserveCommand ToCommandFromResource(UpdateReserveResource resource)
    {
        var time = TimeSpan.Parse(resource.Time);
        var status = Enum.Parse<ReservationStatus>(resource.Status, true);
        
        return new UpdateReserveCommand(
            resource.Id,
            resource.Date,
            time,
            resource.Description,
            resource.ClientId,
            resource.CategoryId,
            resource.TechnicianId,
            status
        );
    }
}