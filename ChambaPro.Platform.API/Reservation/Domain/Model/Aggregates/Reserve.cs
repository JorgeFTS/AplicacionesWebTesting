using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

namespace ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;

public partial class Reserve
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan Time { get; private set; }
    public string Description { get; private set; }
    public int ClientId { get; private set; }
    public string CategoryId { get; private set; }
    public int? TechnicianId { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reserve()
    {
        Description = string.Empty;
        ClientId = 0;
        CategoryId = string.Empty;
        Status = ReservationStatus.Pending;
    }

    public Reserve(DateTime date, TimeSpan time, string description, int clientId, string categoryId)
    {
        Date = date;
        Time = time;
        Description = description;
        ClientId = clientId;
        CategoryId = categoryId;
        Status = ReservationStatus.Pending;
    }

    public Reserve(CreateReserveCommand command)
    {
        Date = command.Date;
        Time = command.Time;
        Description = command.Description;
        ClientId = command.ClientId;
        CategoryId = command.CategoryId;
        Status = ReservationStatus.Pending;
    }

    public void UpdateFromCommand(UpdateReserveCommand command)
    {
        Date = command.Date;
        Time = command.Time;
        Description = command.Description;
        ClientId = command.ClientId;
        CategoryId = command.CategoryId;
        TechnicianId = command.TechnicianId;
        Status = command.Status;
    }

    public bool CanBeCancelled()
    {
        return Status != ReservationStatus.Cancelled && Status != ReservationStatus.Completed;
    }

    public void Cancel()
    {
        if (!CanBeCancelled())
            throw new InvalidOperationException("Reservation cannot be cancelled");
        
        Status = ReservationStatus.Cancelled;
    }

    public void AssignTechnician(int technicianId)
    {
        if (Status == ReservationStatus.Cancelled)
            throw new InvalidOperationException("Cannot assign technician to cancelled reservation");
        
        TechnicianId = technicianId;
        Status = ReservationStatus.Assigned;
    }

    public void Complete()
    {
        if (Status == ReservationStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled reservation");
        
        Status = ReservationStatus.Completed;
    }
}

public enum ReservationStatus
{
    Pending,
    Assigned,
    Completed,
    Cancelled
}
