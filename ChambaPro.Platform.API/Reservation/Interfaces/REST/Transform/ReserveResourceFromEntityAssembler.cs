using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;

public static class ReserveResourceFromEntityAssembler
{
    public static ReserveResource ToResourceFromEntity(Reserve entity)
    {
        return new ReserveResource(
            entity.Id,
            entity.Date,
            entity.Time.ToString(@"hh\:mm"),
            entity.Description,
            entity.ClientId,
            string.Empty, // ClientName populated later if needed
            entity.CategoryId,
            entity.TechnicianId,
            entity.Status.ToString()
        );
    }

    public static ReserveResource ToResourceFromEntityWithClient(Reserve entity, string clientName)
    {
        return new ReserveResource(
            entity.Id,
            entity.Date,
            entity.Time.ToString(@"hh\:mm"),
            entity.Description,
            entity.ClientId,
            clientName,
            entity.CategoryId,
            entity.TechnicianId,
            entity.Status.ToString()
        );
    }
}
