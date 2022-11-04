using MediatR;
using ParkingManager.Application.Contracts.Repository;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Features.Queries
{
    public class GetSummaryQueryHandler : IRequestHandler<GetSummaryQuery, ParkingLotSummaryVm>
    {
        private readonly IParkingLotRepository _parkingLotRepository;

        public GetSummaryQueryHandler(IParkingLotRepository repository)
        {
            _parkingLotRepository = repository;
        }

        public Task<ParkingLotSummaryVm> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
        {
            var parkingLot = _parkingLotRepository.Get();
            return MapToSummary(parkingLot);
            //return _mapper.Map<ParkingLotSummaryVm>(parkingLot);
        }

        private Task<ParkingLotSummaryVm> MapToSummary(ParkingLot parkingLot)
        {
            var summary = new ParkingLotSummaryVm
            {
                TotalSpots = parkingLot.TotalSpots,
                OpenSpots = parkingLot.OpenSpots,
                IsFull = parkingLot.IsFull,
                IsEmpty = parkingLot.IsEmpty,
                OpenSmallSpots = parkingLot.OpenSpotsBySize(SpotSize.Small),
                OpenMediumSpots = parkingLot.OpenSpotsBySize(SpotSize.Medium),
                OpenLargeSpots = parkingLot.OpenSpotsBySize(SpotSize.Large),
                CanParkMotorcycles = parkingLot.CanPark(VehicleType.Motorcycle),
                CanParkCars = parkingLot.CanPark(VehicleType.Car),
                CanParkVans = parkingLot.CanPark(VehicleType.Van),
                MotorcyclesParked = parkingLot.ParkedSpotsByType(VehicleType.Motorcycle),
                CarsParked = parkingLot.ParkedSpotsByType(VehicleType.Car),
                VansParked = parkingLot.ParkedSpotsByType(VehicleType.Van)
            };
            return Task.FromResult(summary);
        }
    }
}
