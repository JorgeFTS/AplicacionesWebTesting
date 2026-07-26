using ChambaPro.Platform.API.Service.Domain.Model.Commands;
using ChambaPro.Platform.API.Service.Domain.Model.Queries;
using ChambaPro.Platform.API.Service.Domain.Service;
using ChambaPro.Platform.API.Service.Interfaces.REST.Resources;
using ChambaPro.Platform.API.Service.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace ChambaPro.Platform.API.Service.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceCommandService _serviceCommandService;
    private readonly IServiceQueryService _serviceQueryService;

    public ServicesController(
        IServiceCommandService serviceCommandService,
        IServiceQueryService serviceQueryService)
    {
        _serviceCommandService = serviceCommandService;
        _serviceQueryService = serviceQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var query = new GetAllServicesQuery();
        var services = await _serviceQueryService.Handle(query);
        var resources = ServiceResourceFromEntityAssembler.ToResourceFromEntity(services);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var query = new GetServiceByIdQuery(id);
        var service = await _serviceQueryService.Handle(query);
        
        if (service == null)
            return NotFound($"Service with id {id} not found");

        var resource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
        return Ok(resource);
    }

    [HttpGet("client/{clientId:int}")]
    public async Task<IActionResult> GetServicesByClientId(int clientId)
    {
        var query = new GetServicesByClientIdQuery(clientId);
        var services = await _serviceQueryService.Handle(query);
        var resources = ServiceResourceFromEntityAssembler.ToResourceFromEntity(services);
        return Ok(resources);
    }

    [HttpGet("technician/{technicianId:int}")]
    public async Task<IActionResult> GetServicesByTechnicianId(int technicianId)
    {
        var query = new GetServicesByTechnicianIdQuery(technicianId);
        var services = await _serviceQueryService.Handle(query);
        var resources = ServiceResourceFromEntityAssembler.ToResourceFromEntity(services);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = CreateServiceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var service = await _serviceCommandService.Handle(command);
        
        if (service == null)
            return BadRequest("Failed to create service");

        var serviceResource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
        return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, serviceResource);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = UpdateServiceCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var service = await _serviceCommandService.Handle(command);
            
            if (service == null)
                return NotFound($"Service with id {id} not found");

            var serviceResource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
            return Ok(serviceResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the service: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var command = new DeleteServiceCommand(id);
        var service = await _serviceCommandService.Handle(command);
        
        if (service == null)
            return NotFound($"Service with id {id} not found");

        return NoContent();
    }

    [HttpPatch("{id:int}/complete")]
    public async Task<IActionResult> CompleteService(int id)
    {
        var command = new CompleteServiceCommand(id);
        var service = await _serviceCommandService.Handle(command);
        
        if (service == null)
            return NotFound($"Service with id {id} not found");

        var serviceResource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
        return Ok(serviceResource);
    }

    [HttpPatch("{id:int}/confirm")]
    public async Task<IActionResult> ConfirmService(int id)
    {
        var command = new ConfirmServiceCommand(id);
        var service = await _serviceCommandService.Handle(command);
        
        if (service == null)
            return NotFound($"Service with id {id} not found");

        var serviceResource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
        return Ok(serviceResource);
    }

    [HttpPatch("{id:int}/cancel")]
    public async Task<IActionResult> CancelService(int id)
    {
        var command = new CancelServiceCommand(id);
        var service = await _serviceCommandService.Handle(command);
        
        if (service == null)
            return NotFound($"Service with id {id} not found");

        var serviceResource = ServiceResourceFromEntityAssembler.ToResourceFromEntity(service);
        return Ok(serviceResource);
    }
}