using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Services;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.Reservation.Application.Internal.CommandServices;

public class ReserveCommandService(IReserveRepository repository, IUnitOfWork unitOfWork) 
    : IReserveCommandService
{
    public async Task<Reserve?> Handle(CreateReserveCommand command)
    {
        
        if (command.Date < DateTime.UtcNow.Date)
            throw new Exception("Reservation date must be in the future");
        
        if (command.Date > DateTime.UtcNow.AddDays(30))
            throw new Exception("Reservations cannot be made more than 30 days in advance");
        
        if (string.IsNullOrWhiteSpace(command.Description) || command.Description.Length < 10)
            throw new Exception("Description must be at least 10 characters long");
        
        var existingReservations = await repository.FindByClientIdAsync(command.ClientId);
        var pendingCount = existingReservations.Count(r => r.Status == ReservationStatus.Pending);
        if (pendingCount >= 5)
            throw new Exception("Client cannot have more than 5 pending reservations");
        
        var reserve = new Reserve(command);
        await repository.AddAsync(reserve);
        await unitOfWork.CompleteAsync();
        return reserve;
    }

    public async Task<Reserve?> Handle(UpdateReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.ReserveId);
        
        if (reserve == null)
            return null;
        
        if (reserve.Status == ReservationStatus.Completed || reserve.Status == ReservationStatus.Cancelled)
            throw new Exception("Cannot modify completed or cancelled reservations");
        
        if (command.Status == ReservationStatus.Completed && reserve.Status != ReservationStatus.Assigned)
            throw new Exception("Only assigned reservations can be completed");
        
        if (command.Date < DateTime.UtcNow.Date)
            throw new Exception("Reservation date must be in the future");

        reserve.UpdateFromCommand(command);

        try
        {
            repository.Update(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update reserve {command.ReserveId}", e);
        }

        return reserve;
    }

    public async Task<Reserve?> Handle(DeleteReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.Id);
        
        if (reserve == null)
            return null;

        try
        {
            repository.Remove(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete reserve {command.Id}", e);
        }
        
        return reserve;
    }

    public async Task<Reserve?> Handle(CancelReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.ReserveId);
        
        if (reserve == null)
            return null;
        
        var reservationDateTime = reserve.Date.Add(reserve.Time);
        if (reservationDateTime.Subtract(DateTime.UtcNow).TotalHours < 2)
            throw new Exception("Cannot cancel reservation less than 2 hours before scheduled time");

        reserve.Cancel();

        try
        {
            repository.Update(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to cancel reserve {command.ReserveId}", e);
        }

        return reserve;
    }
}