using ParkingManager.Domain.Entities;

namespace ParkingManager.Application.Contracts.Repository;

public interface IParkingLotRepository
{
    ParkingLot Get();
    Task Update(ParkingLot parkingLot);
}
