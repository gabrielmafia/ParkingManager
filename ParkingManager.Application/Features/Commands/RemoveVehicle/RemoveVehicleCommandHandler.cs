using MediatR;
using ParkingManager.Application.Contracts.Repository;

namespace ParkingManager.Application.Features.Commands.InsertMovement;

public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand, Unit>
{
    private readonly IParkingLotRepository _parkingLotRepository;

    public RemoveVehicleCommandHandler(IParkingLotRepository parkingLotRepository)
    {
        _parkingLotRepository = parkingLotRepository;
    }

    public async Task<Unit> Handle(RemoveVehicleCommand command, CancellationToken cancellationToken)
    {
        var parkingLot = _parkingLotRepository.Get();
        var validator = new RemoveVehicleCommandValidator(parkingLot);
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
            throw new Exceptions.ValidationException(validationResult);

        parkingLot.Remove(command.VehicleType);

        return Unit.Value;
    }
}
