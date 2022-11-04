using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities
{
    public class ParkingLot
    {
        private const int MediumSpotsForVans = 3;

        public Guid Id { get; set; }
        public List<Spot> Spots { get; set; }
        private List<Spot> AvailableSpots => Spots.Where(s => s.Available).ToList();

        public int TotalSpots => Spots.Count();
        public int OpenSpots => Spots.Count(s => s.Available);
        public int ParkedSpots => Spots.Count(s => !s.Available);

        public bool IsFull => !Spots.Any(s => s.Available);
        public bool IsEmpty => TotalSpots == OpenSpots;
        
        public int OpenSpotsBySize(Nullable<SpotSize> size) => Spots.Count(s => s.Available && s.Size == size);
        public int ParkedSpotsByType(VehicleType type) => Spots.Count(s => s.VehicleParked == type) - 2*(type == VehicleType.Van ? VansParkedInMediumSpots : 0);

        public bool SmallSpotAvailable => Spots.Any(s => s.Available);
        public bool MediumSpotAvailable => Spots.Any(s => s.Available && s.Size == SpotSize.Medium);
        public bool LargeSpotAvailable => Spots.Any(s => s.Available && s.Size == SpotSize.Large);
        
        private bool CanParkVanInMediumSpots => Spots.Count(s => s.Available && s.Size == SpotSize.Medium) >= MediumSpotsForVans;
        private int VansParkedInMediumSpots => Spots.Count(s => s.Size == SpotSize.Medium && s.VehicleParked == VehicleType.Van) / MediumSpotsForVans;

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

        public bool CanPark(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Motorcycle:
                    return SmallSpotAvailable || MediumSpotAvailable || LargeSpotAvailable;
                case VehicleType.Car:
                    return MediumSpotAvailable || LargeSpotAvailable;
                case VehicleType.Van:
                    return LargeSpotAvailable || CanParkVanInMediumSpots;
                default:
                    return false;
            }
        }

        public bool CanRemove(VehicleType vehicle)
        {
            return Spots.Any(s => s.VehicleParked == vehicle);
        }

        public void Park(VehicleType vehicle)
        {
            Spot availableSpot;
            switch (vehicle)
            {
                case VehicleType.Motorcycle:
                    availableSpot = AvailableSpots.OrderBy(s => s.Size).First();
                    availableSpot.Park(vehicle);
                    break;                
                case VehicleType.Car:
                    availableSpot = AvailableSpots.Where(s => s.Size != SpotSize.Small).OrderBy(s => s.Size).First();
                    availableSpot.Park(vehicle);
                    break;                
                case VehicleType.Van:
                    var availableLargeSpots = AvailableSpots.Where(s => s.Size == SpotSize.Large).OrderBy(s => s.Size);
                    if (availableLargeSpots.Any())
                    {
                        availableSpot = availableLargeSpots.First();
                        availableSpot.Park(vehicle);
                        break;
                    }
                    var availableMediumSpots = AvailableSpots.Where(s => s.Size == SpotSize.Medium).Take(MediumSpotsForVans).ToList();
                    availableMediumSpots.ForEach(s => s.Park(vehicle));
                    break;
            }
        }

        public void Remove(VehicleType vehicle)
        {
            var occupiedSpots = Spots.Where(s => s.VehicleParked == vehicle).OrderByDescending(s => s.Size);
            switch (vehicle)
            {
                case VehicleType.Motorcycle:
                    occupiedSpots.First().RemoveVehicle();
                    break;
                case VehicleType.Car:
                    occupiedSpots.First().RemoveVehicle();
                    break;
                case VehicleType.Van:
                    var occupiedMediumSpots = occupiedSpots.Where(s => s.Size == SpotSize.Medium).Take(MediumSpotsForVans).ToList();
                    if(occupiedMediumSpots.Any())
                    {
                        occupiedMediumSpots.ForEach(s => s.RemoveVehicle());
                        break;
                    }
                    occupiedSpots.First().RemoveVehicle();
                    break;
            }
        }
    }
}
