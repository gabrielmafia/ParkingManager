using MediatR;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Features.Commands.InsertMovement
{
    public class RemoveVehicleCommand : IRequest
    {
        public VehicleType VehicleType { get; set; }
    }
}