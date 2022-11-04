using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain
{
    public class Spot
    {
        public Guid Id { get; set; }
        public SpotSize Size { get; set; }
        public Nullable<VehicleType> VehicleParked { get; private set; }

        public Spot(SpotSize spotSize)
        {
            Size = spotSize;
        }

        public bool Available => VehicleParked == null;
        public void Park(VehicleType type) => VehicleParked = type;
        public void RemoveVehicle() => VehicleParked = null;
    }
}
