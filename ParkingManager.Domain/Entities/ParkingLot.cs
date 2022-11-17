using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities;

public class ParkingLot
{
    public Guid Id { get; set; }
    public List<Spot> Spots { get; set; }
    private List<Spot> AvailableSpots => Spots.Where(s => s.Available).ToList();

    public int TotalSpots => Spots.Count();
    public int OpenSpots => Spots.Count(s => s.Available);
    public int ParkedSpots => Spots.Count(s => !s.Available);

    public bool IsFull => !Spots.Any(s => s.Available);
    public bool IsEmpty => TotalSpots == OpenSpots;
        
    public int OpenSpotsBySize(Nullable<SpotSize> size) => Spots.Count(s => s.Available && s.Size == size);

    public ParkingLot(int small, int medium, int large)
    {
        Spots = new List<Spot>();
        AddSpots(SpotSize.Small, small);
        AddSpots(SpotSize.Medium, medium);
        AddSpots(SpotSize.Large, large);
    }

    private void AddSpots(SpotSize size, int amount)
    {
        for (int i = 0; i < amount; i++)
            AddSpot(size);
    }

    private void AddSpot(SpotSize size) => Spots.Add(new Spot(size));

    public int ParkedSpotsByType(VehicleType vehicleType)
    {
        int parked = 0;
        var vehicle = Vehicles.ByType[vehicleType];
        
        foreach (var size in vehicle.GetParkingSizeOrderPreference())
        {
            parked += Spots.Count(s => s.VehicleParked == vehicleType && s.Size == size) / vehicle.OccupiedSpotsBySizeType[size];
        }
        
        return parked;
    }

    public bool CanPark(VehicleType vehicleType)
    {
        var vehicle = Vehicles.ByType[vehicleType];
        foreach (var size in vehicle.GetParkingSizeOrderPreference())
        {
            if(Spots.Count(s => s.Available && s.Size == size) >= vehicle.OccupiedSpotsBySizeType[size])
                return true;
        }
        return false;
    }

    public bool CanRemove(VehicleType vehicleType)
    {
        return Spots.Any(s => s.VehicleParked == vehicleType);
    }

    public void Park(VehicleType vehicleType)
    {
        var vehicle = Vehicles.ByType[vehicleType];
        foreach (var size in vehicle.GetParkingSizeOrderPreference())
        {
            var occupiedSpotsBySizeType = vehicle.OccupiedSpotsBySizeType[size];

            var availableBySize = AvailableSpots.Where(s => s.Size == size).Take(occupiedSpotsBySizeType).ToList();
            if (availableBySize.Count == occupiedSpotsBySizeType)
            {
                availableBySize.ForEach(s => s.Park(vehicleType));
                return;
            }
        }
    }

    public void Remove(VehicleType vehicleType)
    {
        var occupiedSpots = Spots.Where(s => s.VehicleParked == vehicleType).OrderByDescending(s => s.Size);
        var vehicle = Vehicles.ByType[vehicleType];
        var orderPreference = vehicle.GetParkingSizeOrderPreference();
        orderPreference.Reverse();
        foreach (var size in orderPreference)
        {
            var occupiedSizeSpots = occupiedSpots.Where(s => s.Size == size).Take(vehicle.OccupiedSpotsBySizeType[size]).ToList();
            if(occupiedSizeSpots.Any())
            {
                occupiedSizeSpots.ForEach(s => s.RemoveVehicle());
            }
        }
    }
}