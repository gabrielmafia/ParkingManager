using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.Application.Features.Commands.AddVehicle;
using ParkingManager.Application.Features.Commands.InsertMovement;
using ParkingManager.Application.Features.Queries;

namespace ParkingManager.Controllers;

[ApiController]
[Route("[controller]")]
public class SpotController : ControllerBase
{
    private readonly ILogger<SpotController> _logger;
    private readonly IMediator _mediator;


    public SpotController(ILogger<SpotController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("summary", Name = "Summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ParkingLotSummaryVm>> GetSpotsSummary()
    {
        var summaryVm = await _mediator.Send(new GetSummaryQuery());
        return Ok(summaryVm);
    }

    [HttpPatch("add", Name = "Add Vehicle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> AddVehicle([FromBody] AddVehicleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPatch("remove", Name = "Remove Vehicle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Unit>> RemoveVehicle([FromBody] RemoveVehicleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
