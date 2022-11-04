using FluentValidation;
using ParkingManager.Domain.Entities;

namespace ParkingManager.Application.Features.Commands.InsertMovement
{
    public class RemoveVehicleCommandValidator : AbstractValidator<RemoveVehicleCommand>
    {
        public RemoveVehicleCommandValidator(ParkingLot parkingLot)
        {
            RuleFor(p => p.VehicleType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .IsInEnum().WithMessage("{PropertyName} must be Motorcycle, Car or Van");

            RuleFor(e => e)
                .Must(e => parkingLot.CanRemove(e.VehicleType))
                .WithMessage("There's no vehicle of that type parked.");
        }
    }
}
