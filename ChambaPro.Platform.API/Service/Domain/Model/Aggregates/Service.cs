using ChambaPro.Platform.API.Service.Domain.Model.Commands;

namespace ChambaPro.Platform.API.Service.Domain.Model.Aggregates;

public partial class Services
{
    public int Id { get; private set; }
    public int ClientId { get; private set; }
    public int TechnicianId { get; private set; }
    public DateTime Date { get; private set; }
    public string Time { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public ServiceStatus Status { get; private set; }
    public decimal Cost { get; private set; }
    public string Duration { get; private set; }
    public string Address { get; private set; }

    public Services()
    {
        Time = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
        Duration = string.Empty;
        Address = string.Empty;
        Status = ServiceStatus.Pending;
    }

    public Services(int clientId, int technicianId, DateTime date, string time, string description, 
        string category, decimal cost, string duration, string address)
    {
        ClientId = clientId;
        TechnicianId = technicianId;
        Date = date;
        Time = time;
        Description = description;
        Category = category;
        Cost = cost;
        Duration = duration;
        Address = address;
        Status = ServiceStatus.Pending;
    }

    public Services(CreateServiceCommand command)
    {
        ClientId = command.ClientId;
        TechnicianId = command.TechnicianId;
        Date = command.Date;
        Time = command.Time;
        Description = command.Description;
        Category = command.Category;
        Cost = command.Cost;
        Duration = command.Duration;
        Address = command.Address;
        Status = ServiceStatus.Pending;
    }

    public void UpdateFromCommand(UpdateServiceCommand command)
    {
        Date = command.Date;
        Time = command.Time;
        Description = command.Description;
        Category = command.Category;
        Cost = command.Cost;
        Duration = command.Duration;
        Address = command.Address;
        Status = command.Status;
    }

    public void Complete()
    {
        if (Status == ServiceStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled service");
        
        Status = ServiceStatus.Completed;
    }

    public void Confirm()
    {
        if (Status == ServiceStatus.Cancelled)
            throw new InvalidOperationException("Cannot confirm a cancelled service");
        
        Status = ServiceStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == ServiceStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed service");
        
        Status = ServiceStatus.Cancelled;
    }
}

public enum ServiceStatus
{
    Pending,
    Confirmed,
    Completed,
    Cancelled
}