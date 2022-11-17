using Moq;
using ParkingManager.Application.Contracts.Repository;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.UnitTests.Mocks;

public class RepositoryMocks
{
    public static Mock<IParkingLotRepository> GetEmptyParkingLotRepository()
    {
        var parkingLot = new ParkingLot(3, 4, 5);

        var mockCategoryRepository = new Mock<IParkingLotRepository>();
        mockCategoryRepository.Setup(repo => repo.Get()).Returns(parkingLot);

        return mockCategoryRepository;
    }
    public static Mock<IParkingLotRepository> GetFullParkingLotRepository()
    {
        var parkingLot = new ParkingLot(3, 3, 3);

        for(var i = 0; i < 3; i++)
        {
            parkingLot.Park(VehicleType.Motorcycle);
            parkingLot.Park(VehicleType.Car);
            parkingLot.Park(VehicleType.Van);
        }

        var mockCategoryRepository = new Mock<IParkingLotRepository>();
        mockCategoryRepository.Setup(repo => repo.Get()).Returns(parkingLot);

        return mockCategoryRepository;
    }
    public static Mock<IParkingLotRepository> GetParkingHalfFullLotRepository()
    {
        var parkingLot = new ParkingLot(1, 5, 1);

        parkingLot.Park(VehicleType.Motorcycle);
        parkingLot.Park(VehicleType.Van);
        parkingLot.Park(VehicleType.Van);

        var mockCategoryRepository = new Mock<IParkingLotRepository>();
        mockCategoryRepository.Setup(repo => repo.Get()).Returns(parkingLot);

        return mockCategoryRepository;
    }
}
