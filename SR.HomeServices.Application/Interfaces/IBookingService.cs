
using SR.HomeServices.Domain.Enums;

namespace SR.HomeServices.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDashboardVm> GetDashboardAsync(int captainId);
        Task<IEnumerable<BookingItemVm>> GetBookingsByStatusAsync(int captainId, BookingStatus status);
    }
}
