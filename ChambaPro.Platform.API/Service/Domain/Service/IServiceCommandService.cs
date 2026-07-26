using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Commands;

namespace ChambaPro.Platform.API.Service.Domain.Service;

/// <summary>
/// Service Command Service Interface
/// </summary>
public interface IServiceCommandService
{
    /// <summary>
    /// Handle create service command
    /// </summary>
    /// <param name="command">The CreateServiceCommand</param>
    /// <returns>The created Service or null</returns>
    Task<Services?> Handle(CreateServiceCommand command);
    
    /// <summary>
    /// Handle update service command
    /// </summary>
    /// <param name="command">The UpdateServiceCommand</param>
    /// <returns>The updated Service or null</returns>
    Task<Services?> Handle(UpdateServiceCommand command);
    
    /// <summary>
    /// Handle delete service command
    /// </summary>
    /// <param name="command">The DeleteServiceCommand</param>
    /// <returns>The deleted Service or null</returns>
    Task<Services?> Handle(DeleteServiceCommand command);
    
    /// <summary>
    /// Handle complete service command
    /// </summary>
    /// <param name="command">The CompleteServiceCommand</param>
    /// <returns>The completed Service or null</returns>
    Task<Services?> Handle(CompleteServiceCommand command);
    
    /// <summary>
    /// Handle confirm service command
    /// </summary>
    /// <param name="command">The ConfirmServiceCommand</param>
    /// <returns>The confirmed Service or null</returns>
    Task<Services?> Handle(ConfirmServiceCommand command);
    
    /// <summary>
    /// Handle cancel service command
    /// </summary>
    /// <param name="command">The CancelServiceCommand</param>
    /// <returns>The cancelled Service or null</returns>
    Task<Services?> Handle(CancelServiceCommand command);
}