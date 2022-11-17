using Microsoft.Extensions.Caching.Memory;
using ParkingManager.Application.Contracts.Repository;
using ParkingManager.Domain.Entities;

namespace Persistence;

public class ParkingLotRepository : IParkingLotRepository
{
    private readonly IMemoryCache _memoryCache;
    private ParkingLot NewParkingLot() => new ParkingLot(1, 10, 2);
    private MemoryCacheEntryOptions cacheOptions => new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1));

    public ParkingLotRepository(IMemoryCache cache)
    {
        _memoryCache = cache;
    }

    public ParkingLot Get()
    {
        ParkingLot parkingLot;

        if(!_memoryCache.TryGetValue(typeof(ParkingLot), out var cacheValue))
        {
            parkingLot = NewParkingLot();
            _memoryCache.Set(typeof(ParkingLot), parkingLot, cacheOptions);
            return parkingLot;
        }

        parkingLot = (ParkingLot)(cacheValue ?? NewParkingLot()) ;
        return parkingLot;
    }

    public Task Update(ParkingLot parkingLot)
    {
        _memoryCache.Set(typeof(ParkingLot), parkingLot, cacheOptions);
        return Task.CompletedTask;
    }

}