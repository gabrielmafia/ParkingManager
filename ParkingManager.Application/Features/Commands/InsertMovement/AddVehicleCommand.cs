using MediatR;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Features.Commands.AddVehicle;

public class AddVehicleCommand : IRequest
{
    public VehicleType VehicleType { get; set; }
}
