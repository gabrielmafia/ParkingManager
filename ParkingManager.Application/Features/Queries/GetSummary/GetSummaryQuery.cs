using MediatR;

namespace ParkingManager.Application.Features.Queries
{
    public class GetSummaryQuery :IRequest<ParkingLotSummaryVm>
    {
    }
}
