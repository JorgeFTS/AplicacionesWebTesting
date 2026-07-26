using ChambaPro.Platform.API.Service.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;

namespace ChambaPro.Platform.API.Service.Domain.Service;

/// <summary>
/// Service Query Service Interface
/// </summary>
public interface IServiceQueryService
{
    /// <summary>
    /// Handle get all services query
    /// </summary>
    /// <param name="query">The GetAllServicesQuery</param>
    /// <returns>List of all Services</returns>
    Task<IEnumerable<Services>> Handle(GetAllServicesQuery query);
    
    /// <summary>
    /// Handle get service by id query
    /// </summary>
    /// <param name="query">The GetServiceByIdQuery</param>
    /// <returns>Service or null</returns>
    Task<Services?> Handle(GetServiceByIdQuery query);
    
    /// <summary>
    /// Handle get services by client id query
    /// </summary>
    /// <param name="query">The GetServicesByClientIdQuery</param>
    /// <returns>List of Services for the client</returns>
    Task<IEnumerable<Services>> Handle(GetServicesByClientIdQuery query);
    
    /// <summary>
    /// Handle get services by technician id query
    /// </summary>
    /// <param name="query">The GetServicesByTechnicianIdQuery</param>
    /// <returns>List of Services for the technician</returns>
    Task<IEnumerable<Services>> Handle(GetServicesByTechnicianIdQuery query);
}
