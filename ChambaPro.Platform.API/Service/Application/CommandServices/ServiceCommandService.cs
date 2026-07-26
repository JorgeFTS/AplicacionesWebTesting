using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Commands;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Service.Domain.Service;
using ChambaPro.Platform.API.Shared.Domain.Repositories;

namespace ChambaPro.Platform.API.Service.Application.CommandServices;

/// <summary>
/// Service Command Service Implementation
/// </summary>
public class ServiceCommandService(IServiceRepository repository, IUnitOfWork unitOfWork) 
    : IServiceCommandService
{
    /// <inheritdoc />
    public async Task<Services?> Handle(CreateServiceCommand command)
    {
        var service = new Services(command);
        try
        {
            await repository.AddAsync(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating service: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Services?> Handle(UpdateServiceCommand command)
    {
        var service = await repository.FindByIdAsync(command.Id);
        
        if (service == null)
            return null;

        service.UpdateFromCommand(command);

        try
        {
            repository.Update(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update service {command.Id}", e);
        }
    }

    /// <inheritdoc />
    public async Task<Services?> Handle(DeleteServiceCommand command)
    {
        var service = await repository.FindByIdAsync(command.Id);
        
        if (service == null)
            return null;

        try
        {
            repository.Remove(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete service {command.Id}", e);
        }
    }

    /// <inheritdoc />
    public async Task<Services?> Handle(CompleteServiceCommand command)
    {
        var service = await repository.FindByIdAsync(command.Id);
        
        if (service == null)
            return null;

        try
        {
            service.Complete();
            repository.Update(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to complete service {command.Id}", e);
        }
    }

    /// <inheritdoc />
    public async Task<Services?> Handle(ConfirmServiceCommand command)
    {
        var service = await repository.FindByIdAsync(command.Id);
        
        if (service == null)
            return null;

        try
        {
            service.Confirm();
            repository.Update(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to confirm service {command.Id}", e);
        }
    }

    /// <inheritdoc />
    public async Task<Services?> Handle(CancelServiceCommand command)
    {
        var service = await repository.FindByIdAsync(command.Id);
        
        if (service == null)
            return null;

        try
        {
            service.Cancel();
            repository.Update(service);
            await unitOfWork.CompleteAsync();
            return service;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to cancel service {command.Id}", e);
        }
    }
}