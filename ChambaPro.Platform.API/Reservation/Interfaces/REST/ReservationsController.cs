using System.Net.Mime;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Domain.Model.Queries;
using ChambaPro.Platform.API.Reservation.Domain.Services;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Operations for managing reservations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReserveCommandService _reserveCommandService;
    private readonly IReserveQueryService _reserveQueryService;
    private readonly IUserQueryService _userQueryService;
    private readonly IStringLocalizer<ReservationsController> _localizer;

    public ReservationsController(
        IReserveCommandService reserveCommandService,
        IReserveQueryService reserveQueryService,
        IUserQueryService userQueryService,
        IStringLocalizer<ReservationsController> localizer)
    {
        _reserveCommandService = reserveCommandService;
        _reserveQueryService = reserveQueryService;
        _userQueryService = userQueryService;
        _localizer = localizer;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new Reservation",
        Description = "Creates a new Reservation",
        OperationId = "CreateReservation")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Reservation Was Created", typeof(ReserveResource))]
    public async Task<ActionResult> CreateReservation([FromBody] CreateReserveResource resource)
    {
        var createReserveCommand = CreateReserveCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _reserveCommandService.Handle(createReserveCommand);

        if (result == null)
            return BadRequest(_localizer["ReservationCreationFailed"]);

        return CreatedAtAction(
            nameof(GetReservationById),
            new { id = result.Id },
            ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Gets a Reservation by Id",
        Description = "Gets a Reservation by Id",
        OperationId = "GetReservationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Found", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> GetReservationById(int id)
    {
        var getReserveByIdQuery = new GetReserveByIdQuery(id);
        var result = await _reserveQueryService.Handle(getReserveByIdQuery);

        if (result == null)
            return NotFound(_localizer["ReservationNotFound"]);

        // Fetch client name
        var client = await _userQueryService.Handle(new ChambaPro.Platform.API.IAM.Domain.Model.Queries.GetUserByIdQuery(result.ClientId));
        var clientName = client != null ? $"{client.Name} {client.LastName}" : "Unknown Client";

        return Ok(ReserveResourceFromEntityAssembler.ToResourceFromEntityWithClient(result, clientName));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all Reservations or filters by clientId/technicianId",
        Description = "Gets all Reservations or filters by clientId/technicianId",
        OperationId = "GetAllReservations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservations Were Found", typeof(IEnumerable<ReserveResource>))]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] int? clientId,
        [FromQuery] int? technicianId)
    {
        try
        {
            if (clientId.HasValue)
            {
                var getReservesByClientIdQuery = new GetReservesByClientIdQuery(clientId.Value);
                var clientReserves = await _reserveQueryService.Handle(getReservesByClientIdQuery);

                // Fetch client name for the client
                var client = await _userQueryService.Handle(new ChambaPro.Platform.API.IAM.Domain.Model.Queries.GetUserByIdQuery(clientId.Value));
                var clientName = client != null ? $"{client.Name} {client.LastName}" : "Unknown Client";

                var clientResult = clientReserves.Select(reserve =>
                    ReserveResourceFromEntityAssembler.ToResourceFromEntityWithClient(reserve, clientName)
                ).ToList();
                return Ok(clientResult);
            }

            if (technicianId.HasValue)
            {
                var getReservesByTechnicianIdQuery = new GetReservesByTechnicianIdQuery(technicianId.Value);
                var technicianReserves = await _reserveQueryService.Handle(getReservesByTechnicianIdQuery);
                var technicianResult = technicianReserves.Select(ReserveResourceFromEntityAssembler.ToResourceFromEntity).ToList();
                return Ok(technicianResult);
            }

            var getAllReservesQuery = new GetAllReservesQuery();
            var reserves = await _reserveQueryService.Handle(getAllReservesQuery);

            // Fetch client names for all reservations
            var clientIds = reserves.Select(r => r.ClientId).Distinct().ToList();
            var clientNames = new Dictionary<int, string>();

            foreach (var id in clientIds)
            {
                var client = await _userQueryService.Handle(new ChambaPro.Platform.API.IAM.Domain.Model.Queries.GetUserByIdQuery(id));
                if (client != null)
                {
                    clientNames[id] = $"{client.Name} {client.LastName}";
                }
            }

            var result = reserves.Select(reserve =>
            {
                var clientName = clientNames.TryGetValue(reserve.ClientId, out var name) ? name : "Unknown Client";
                return ReserveResourceFromEntityAssembler.ToResourceFromEntityWithClient(reserve, clientName);
            }).ToList();

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Updates a Reservation",
        Description = "Updates a Reservation",
        OperationId = "UpdateReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Updated", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> UpdateReservation(int id, [FromBody] UpdateReserveResource resource)
    {
        if (id != resource.Id)
            return BadRequest(_localizer["InvalidReservationId"]);

        var updateReserveCommand = UpdateReserveCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _reserveCommandService.Handle(updateReserveCommand);

        if (result == null)
            return NotFound(_localizer["ReservationNotFound"]);

        return Ok(ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/cancel")]
    [SwaggerOperation(
        Summary = "Cancels a Reservation by Id",
        Description = "Cancels a Reservation with the given Id",
        OperationId = "CancelReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Cancelled", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> CancelReservation(int id)
    {
        var cancelReserveCommand = new CancelReserveCommand(id);
        var result = await _reserveCommandService.Handle(cancelReserveCommand);

        if (result == null)
            return NotFound(_localizer["ReservationNotFound"]);

        return Ok(ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Reservation by Id",
        Description = "Deletes a Reservation with the given Id",
        OperationId = "DeleteReservation")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Reservation Was Deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> DeleteReservation(int id)
    {
        var deleteReserveCommand = new DeleteReserveCommand(id);
        var result = await _reserveCommandService.Handle(deleteReserveCommand);

        if (result == null)
            return NotFound(_localizer["ReservationNotFound"]);

        return NoContent();
    }
}
