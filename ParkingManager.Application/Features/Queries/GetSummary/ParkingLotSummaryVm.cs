namespace ParkingManager.Application.Features.Queries
{
    public class ParkingLotSummaryVm
    {
        public int TotalSpots { get; set; }
        public int OpenSpots { get; set;}
        public bool IsFull { get; set; }
        public bool IsEmpty { get; set; }

        public int OpenSmallSpots { get; set;}
        public int OpenMediumSpots { get; set;}
        public int OpenLargeSpots { get; set;}


        public bool CanParkMotorcycles{ get; set; }
        public bool CanParkCars{ get; set; }
        public bool CanParkVans { get; set; }
        
        public int MotorcyclesParked { get; set; }
        public int CarsParked { get; set; }
        public int VansParked { get; set; }


    }
}