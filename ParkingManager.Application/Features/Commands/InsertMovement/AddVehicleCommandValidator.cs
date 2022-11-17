using FluentValidation;
using ParkingManager.Domain.Entities;

namespace ParkingManager.Application.Features.Commands.AddVehicle;

public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
{
    public AddVehicleCommandValidator(ParkingLot parkingLot)
    {
        RuleFor(p => p.VehicleType)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .IsInEnum().WithMessage("{PropertyName} must be Motorcycle, Car or Van");

        RuleFor(e => e)
            .Must(e => parkingLot.CanPark(e.VehicleType))
            .WithMessage("There's no available spot for the vehicle.");
    }
}