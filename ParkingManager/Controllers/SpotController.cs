using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ParkingManager.Application.Features.Commands.AddVehicle;
using ParkingManager.Application.Features.Commands.InsertMovement;
using ParkingManager.Application.Features.Queries;

namespace ParkingManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotController : ControllerBase
    {
        private readonly ILogger<SpotController> _logger;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;


        public SpotController(ILogger<SpotController> logger, IMediator mediator, IMemoryCache cache)
        {
            _logger = logger;
            _mediator = mediator;
            _cache = cache;
        }

        [HttpGet("summary", Name = "Summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ParkingLotSummaryVm>> GetSpotsSummary()
        {
            var summaryVm = await _mediator.Send(new GetSummaryQuery());
            return Ok(summaryVm);
        }

        [HttpPost("add", Name = "Add Vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> AddVehicle([FromBody] AddVehicleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("remove", Name = "Remove Vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> RemoveVehicle([FromBody] RemoveVehicleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}