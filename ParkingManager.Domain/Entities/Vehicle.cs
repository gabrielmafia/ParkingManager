using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities;

public struct Vehicle
{
    public VehicleType Type { get; private set; }
    public Dictionary<SpotSize, int> OccupiedSpotsBySizeType { get; private set; }

    public Vehicle(VehicleType type, int small, int medium, int large)
    {
        Type = type;
        OccupiedSpotsBySizeType = new()
        {
            { SpotSize.Small, small },
            { SpotSize.Medium, medium },
            { SpotSize.Large, large },
        };
    }

    public List<SpotSize> GetParkingSizeOrderPreference()
    {
        var preferenceOrder = new List<SpotSize>();
        var sortedOrder = from entry in OccupiedSpotsBySizeType orderby entry.Value ascending select entry;
        foreach(var size in sortedOrder)
        {
            if(size.Value > 0)
                preferenceOrder.Add(size.Key);
        }

        return preferenceOrder;
    }
}

public static class Vehicles
{
    public static readonly Vehicle Motorcycle = new(VehicleType.Motorcycle, 1, 1, 1);
    public static readonly Vehicle Car = new(VehicleType.Car, 0, 1, 1);
    public static readonly Vehicle Van = new(VehicleType.Van, 0, 3, 1);
    public static readonly Vehicle Truck = new(VehicleType.Truck, 0, 0, 2);

    public static readonly Dictionary<VehicleType, Vehicle> ByType = new()
    {
        { VehicleType.Motorcycle, Motorcycle },
        { VehicleType.Car, Car },
        { VehicleType.Van, Van },
        { VehicleType.Truck, Truck },
    };
}
