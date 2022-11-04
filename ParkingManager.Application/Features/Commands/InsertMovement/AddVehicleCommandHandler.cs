using MediatR;
using ParkingManager.Application.Contracts.Repository;

namespace ParkingManager.Application.Features.Commands.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Unit>
    {
        private readonly IParkingLotRepository _parkingLotRepository;

        public AddVehicleCommandHandler(IParkingLotRepository parkingLotRepository)
        {
            _parkingLotRepository = parkingLotRepository;
        }

        public async Task<Unit> Handle(AddVehicleCommand command, CancellationToken cancellationToken)
        {
            var parkingLot = _parkingLotRepository.Get();
            var validator = new AddVehicleCommandValidator(parkingLot);
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            parkingLot.Park(command.VehicleType);

            return Unit.Value;
        }
    }
}
