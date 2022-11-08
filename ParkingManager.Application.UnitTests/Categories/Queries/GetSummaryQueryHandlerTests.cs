using Moq;
using ParkingManager.Application.Contracts.Repository;
using ParkingManager.Application.Features.Queries;
using ParkingManager.Application.UnitTests.Mocks;
using System.Threading;
using Xunit;
using Shouldly;


namespace ParkingManager.Application.UnitTests
{
    public class GetSummaryQueryHandlerTests
    {
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepositoryEmpty;
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepositoryFull;
        private readonly Mock<IParkingLotRepository> _mockParkingLotRepositoryHalfFull;

        public GetSummaryQueryHandlerTests()
        {
            _mockParkingLotRepositoryEmpty = RepositoryMocks.GetEmptyParkingLotRepository();
            _mockParkingLotRepositoryFull = RepositoryMocks.GetFullParkingLotRepository();
            _mockParkingLotRepositoryHalfFull = RepositoryMocks.GetParkingHalfFullLotRepository();
        }

        [Fact]
        public async void GetSummaryEmpty()
        {
            var handler = new GetSummaryQueryHandler(_mockParkingLotRepositoryEmpty.Object);

            var result = await handler.Handle(new GetSummaryQuery(), CancellationToken.None);

            result.ShouldBeOfType<ParkingLotSummaryVm>();

            result.TotalSpots.ShouldBe(12);
            result.OpenSpots.ShouldBe(12);

            result.IsFull.ShouldBe(false);
            result.IsEmpty.ShouldBe(true);
            
            result.OpenSmallSpots.ShouldBe(3);
            result.OpenMediumSpots.ShouldBe(4);
            result.OpenLargeSpots.ShouldBe(5);

            result.CanParkMotorcycles.ShouldBe(true);
            result.CanParkCars.ShouldBe(true);
            result.CanParkVans.ShouldBe(true);

            result.MotorcyclesParked.ShouldBe(0);
            result.CarsParked.ShouldBe(0);
            result.VansParked.ShouldBe(0);
        }

        [Fact]
        public async void GetSummaryFull()
        {
            var handler = new GetSummaryQueryHandler(_mockParkingLotRepositoryFull.Object);

            var result = await handler.Handle(new GetSummaryQuery(), CancellationToken.None);

            result.ShouldBeOfType<ParkingLotSummaryVm>();

            result.TotalSpots.ShouldBe(9);
            result.OpenSpots.ShouldBe(0);

            result.IsFull.ShouldBe(true);
            result.IsEmpty.ShouldBe(false);
            
            result.OpenSmallSpots.ShouldBe(0);
            result.OpenMediumSpots.ShouldBe(0);
            result.OpenLargeSpots.ShouldBe(0);

            result.CanParkMotorcycles.ShouldBe(false);
            result.CanParkCars.ShouldBe(false);
            result.CanParkVans.ShouldBe(false);

            result.MotorcyclesParked.ShouldBe(3);
            result.CarsParked.ShouldBe(3);
            result.VansParked.ShouldBe(3);
        }

        [Fact]
        public async void GetSummaryJHalfFull()
        {
            var handler = new GetSummaryQueryHandler(_mockParkingLotRepositoryHalfFull.Object);

            var result = await handler.Handle(new GetSummaryQuery(), CancellationToken.None);

            result.ShouldBeOfType<ParkingLotSummaryVm>();

            result.TotalSpots.ShouldBe(7);
            result.OpenSpots.ShouldBe(2);

            result.IsFull.ShouldBe(false);
            result.IsEmpty.ShouldBe(false);
            
            result.OpenSmallSpots.ShouldBe(0);
            result.OpenMediumSpots.ShouldBe(2);
            result.OpenLargeSpots.ShouldBe(0);

            result.CanParkMotorcycles.ShouldBe(true);
            result.CanParkCars.ShouldBe(true);
            result.CanParkVans.ShouldBe(false);

            result.MotorcyclesParked.ShouldBe(1);
            result.CarsParked.ShouldBe(0);
            result.VansParked.ShouldBe(2);
        }
    }
}