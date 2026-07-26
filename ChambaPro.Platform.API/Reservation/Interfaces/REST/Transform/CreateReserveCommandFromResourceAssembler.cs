using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;

public static class CreateReserveCommandFromResourceAssembler
{
    public static CreateReserveCommand ToCommandFromResource(CreateReserveResource resource)
    {
        TimeSpan time;
    
        if (DateTime.TryParse(resource.Time, out DateTime dateTime))
        {
            time = dateTime.TimeOfDay;
        }
        else if (!TimeSpan.TryParse(resource.Time, out time))
        {
            throw new ArgumentException($"Invalid time format: '{resource.Time}'. " +
                                        "Use time format 'HH:mm' (e.g., '14:30') or valid DateTime string.");
        }
        
        return new CreateReserveCommand(
            resource.Date,
            time,
            resource.Description,
            resource.ClientId,
            resource.CategoryId
        );
    }
}
