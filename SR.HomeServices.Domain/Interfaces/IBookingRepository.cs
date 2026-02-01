using SR.HomeServices.Domain.Entities;
using SR.HomeServices.Domain.Enums;

namespace SR.HomeServices.Domain.Interfaces
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Gets bookings by status for a specific captain.
        /// </summary>
        /// <param name="captainId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IEnumerable<BookingItemVm>> GetBookingsByStatusAsync(int captainId, BookingStatus status);
    }
}
